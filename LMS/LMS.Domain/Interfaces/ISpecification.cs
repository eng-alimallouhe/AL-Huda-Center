using System.Linq.Expressions;

namespace LMS.Domain.Interfaces
{
    public interface ISpecification<TEntity>
    {
        Expression<Func<TEntity, bool>>? Criteria { get; }
        List<Expression<Func<TEntity, object>>> Includes { get; }
        bool IsTrackingEnabled { get; }
        Expression<Func<TEntity, object>>? OrderBy { get; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; }
        int? Skip { get; }
        int? Take { get; }
    }
}
