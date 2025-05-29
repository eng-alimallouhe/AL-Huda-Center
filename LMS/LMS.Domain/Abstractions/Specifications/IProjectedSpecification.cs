using System.Linq.Expressions;

namespace LMS.Domain.Abstractions.Specifications
{
    public interface IProjectedSpecification<TEntity, TResult, TKey> :
        ISpecification<TEntity>
        where TEntity : class
    {
        Expression<Func<TEntity, TKey>> GroupBy { get; }
        Expression<Func<IGrouping<TKey, TEntity>, TResult>> Selector { get; }
        Func<IQueryable<TResult>, IOrderedQueryable<TResult>>? ResultOrdering { get; }
    }
}