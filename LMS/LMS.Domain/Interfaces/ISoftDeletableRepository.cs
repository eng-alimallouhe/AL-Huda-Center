using System.Linq.Expressions;

namespace LMS.Domain.Interfaces
{
    public interface ISoftDeletableRepository<TEntity>
    {
        Task<ICollection<TEntity>> GetAllAsync(ISpecification<TEntity> specification);


        Task<TEntity?> GetBySpecificationAsync(ISpecification<TEntity> specification);


        Task<TEntity?> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity?> GetByIdAsync(Guid id);


        Task<TEntity> AddAsync(TEntity entity);


        Task UpdateAsync(TEntity entity);

        
        Task SoftDeleteAsync(Guid id);


        Task HardDeleteAsync(Guid id);

        Task SaveChangesAsync();
    }
}
