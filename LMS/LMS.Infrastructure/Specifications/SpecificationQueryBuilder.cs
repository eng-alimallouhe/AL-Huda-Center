using LMS.Domain.Abstractions.Specifications;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Specifications
{
    public static class SpecificationQueryBuilder
    {
        public static IQueryable<TEntity> GetQuery<TEntity>(
            IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> specification)
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

            
            query = (specification.Take.HasValue && specification.Skip.HasValue) ?
                        query.Skip(specification.Skip.Value).Take(specification.Take.Value) :
                        query;

            
            foreach (var include in specification.Includes)
            {
                query = query.Include(include);
            }

            return query;
        }
    }
}
