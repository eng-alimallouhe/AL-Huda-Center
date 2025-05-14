using System.Linq.Expressions;

namespace LMS.Domain.Interfaces
{
    public interface ISoftDeletableRepository<TEntity> where TEntity : class
    {
        Task<ICollection<TEntity>> GetAllAsync(ISpecification<TEntity> specification);
        Task<TEntity?> GetBySpecificationAsync(ISpecification<TEntity> specification);
        Task<TEntity?> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task HardDeleteAsync(Guid id);
        Task SoftDeleteAsync(Guid id);
    }
}
