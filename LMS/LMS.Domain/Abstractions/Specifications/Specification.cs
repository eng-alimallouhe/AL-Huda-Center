using System.Linq.Expressions;

namespace LMS.Domain.Abstractions.Specifications
{
    public class Specification<TEntity> : ISpecification<TEntity> where TEntity : class
    {
        public Specification(
            Expression<Func<TEntity, bool>>? criteria = null,
            List<string>? includes = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            Expression<Func<TEntity, object>>? orderByDescending = null,
            int? take = null,
            int? skip = null,
            bool tracking = true)
        {
            Criteria = criteria;
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
    }
}
