using System.Linq.Expressions;

namespace LMS.Domain.Abstractions.Specifications
{
    public class ProjectedSpecification<TEntity, TResult, TKey>
        : IProjectedSpecification<TEntity, TResult, TKey>
        where TEntity : class
        where TResult : class
        where TKey : class
    {
        public ProjectedSpecification(
            Expression<Func<TEntity, TKey>> groupBy,
            Expression<Func<IGrouping<TKey, TEntity>, TResult>> selector,
            Expression<Func<TEntity, bool>>? criteria = null,
            List<string>? includes = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            Expression<Func<TEntity, object>>? orderByDescending = null,
            int? take = null,
            int? skip = null,
            bool tracking = true)
        {
            Criteria = criteria;
            GroupBy = groupBy;
            Selector = selector;
            Includes = includes ?? new List<string>();
            OrderBy = orderBy;
            OrderByDescending = orderByDescending;
            Take = take;
            Skip = skip;
            IsTrackingEnabled = tracking;
        }

        public Expression<Func<TEntity, bool>>? Criteria { get; }
        public List<string> Includes { get; }
        public Expression<Func<TEntity, object>>? OrderBy { get; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; }
        public int? Take { get; }
        public int? Skip { get; }
        public bool IsTrackingEnabled { get; }
        public Expression<Func<TEntity, TKey>> GroupBy { get; }
        public Expression<Func<IGrouping<TKey, TEntity>, TResult>> Selector { get; }

        public Func<IQueryable<TResult>, IOrderedQueryable<TResult>>? ResultOrdering { get; }
    }
}
