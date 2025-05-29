using System.Linq.Expressions;
using LMS.Domain.Abstractions.Specifications;

namespace LMS.Domain.Abstractions.Repositories
{
    public interface IBaseRepository <TEntity> 
        where TEntity : class
    {
        Task<ICollection<TEntity>> GetAllAsync(ISpecification<TEntity> specification);
        Task<TEntity?> GetBySpecificationAsync(ISpecification<TEntity> specification);
        Task<ICollection<TResult>> GetAllProjectedAsync<TResult, TKey>(IProjectedSpecification<TEntity, TResult, TKey> projectedSpecification);
        Task<TEntity?> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> AsQueryable();
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task HardDeleteAsync(Guid id);
    }
}