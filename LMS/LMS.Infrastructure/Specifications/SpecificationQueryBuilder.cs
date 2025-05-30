using LMS.Common.Results;
using LMS.Domain.Abstractions.Specifications;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Specifications
{
    public static class SpecificationQueryBuilder
    {
        public static IQueryable<TEntity> GetQuery<TEntity>(
            IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> specification, 
            bool applyPaging)
            where TEntity : class
        {
            var query = specification.IsTrackingEnabled ?
                inputQuery :
                inputQuery.AsNoTracking();

            
            query = (specification.Criteria == null) ?
                query :
                query.Where(specification.Criteria);


            query = (specification.OrderBy != null) ?
                query = query.OrderBy(specification.OrderBy) :
                (specification.OrderByDescending != null) ?
                    query = query.OrderByDescending(specification.OrderByDescending) :
                    query;

            if (applyPaging)
            {

                query = (specification.Take.HasValue && specification.Skip.HasValue) ?
                            query.Skip((specification.Skip.Value-1) * specification.Take.Value)
                            .Take(specification.Take.Value) :
                            query;
            }


            foreach (var include in specification.Includes)
            {
                query = query.Include(include);
            }

            return query;
        }


        public static IQueryable<TResult> GetQuery<TEntity, TResult, TKey>(
            IQueryable<TEntity> inputQuery,
            IProjectedSpecification<TEntity, TResult, TKey> projectedSpecification)
            where TEntity : class
        {
            var query = projectedSpecification.IsTrackingEnabled ?
                inputQuery :
                inputQuery.AsNoTracking();


            query = (projectedSpecification.Criteria == null) ?
                query :
                query.Where(projectedSpecification.Criteria);


            query = (projectedSpecification.OrderBy != null) ?
                query = query.OrderBy(projectedSpecification.OrderBy) :
                (projectedSpecification.OrderByDescending != null) ?
                    query = query.OrderByDescending(projectedSpecification.OrderByDescending) :
                    query;


            foreach (var include in projectedSpecification.Includes)
            {
                query = query.Include(include);
            }

            if (projectedSpecification.GroupBy != null)
            {
                var groupedQuery = query.GroupBy(projectedSpecification.GroupBy);

                if (projectedSpecification.Selector != null)
                {
                    var projectedQuery = groupedQuery.Select(projectedSpecification.Selector);
                    
                    if (projectedSpecification.ResultOrdering != null)
                    {
                        projectedQuery =  projectedSpecification.ResultOrdering(projectedQuery);
                    }

                    projectedQuery = (projectedSpecification.Take.HasValue && projectedSpecification.Skip.HasValue) ?
                            projectedQuery.Skip((projectedSpecification.Skip.Value - 1) * projectedSpecification.Take.Value)
                            .Take(projectedSpecification.Take.Value) :
                            projectedQuery;
                    
                    Console.WriteLine(projectedQuery.ToQueryString());
                    
                    return projectedQuery;
                }
            }

            
            if (typeof(TEntity) == typeof(TResult))
            {
                return (IQueryable<TResult>)query;
            }
            throw new InvalidCastException();
        }
    }
}
