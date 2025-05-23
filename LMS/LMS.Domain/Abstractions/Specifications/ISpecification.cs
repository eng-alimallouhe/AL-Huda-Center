using System.Linq.Expressions;

namespace LMS.Domain.Abstractions.Specifications
{
    public interface ISpecification<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>>? Criteria { get; }
        List<string> Includes { get; }
        bool IsTrackingEnabled { get; }
        Expression<Func<TEntity, object>>? OrderBy { get; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; }
        int? Skip { get; }
        int? Take { get; }
    }
}
