using System.Linq.Expressions;
using LMS.Domain.Abstractions.Specifications;

namespace LMS.Domain.Abstractions.Repositories
{
    public interface ISoftDeletableRepository<TEntity> where TEntity : class
    {
        Task<ICollection<TEntity>> GetAllAsync(ISpecification<TEntity> specification);
        Task<TEntity?> GetBySpecificationAsync(ISpecification<TEntity> specification);
        Task<TEntity?> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity?> GetByIdAsync(Guid id);
        IQueryable<TEntity> AsQueryable();
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task HardDeleteAsync(Guid id);
        Task SoftDeleteAsync(Guid id);
    }
}
