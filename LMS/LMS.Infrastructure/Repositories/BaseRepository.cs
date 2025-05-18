using System.Linq.Expressions;
using LMS.Common.Exceptions;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Infrastructure.DbContexts;
using LMS.Infrastructure.Specifications;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly LMSDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(LMSDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        
        public async Task<ICollection<TEntity>> GetAllAsync(ISpecification<TEntity> specification)
        {
            var query = SpecificationQueryBuilder.GetQuery(_dbSet, specification);
            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetBySpecificationAsync(ISpecification<TEntity> specification)
        {
            var query = SpecificationQueryBuilder.GetQuery(_dbSet, specification);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<TEntity?> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression)
        {
            var query = _context.Set<TEntity>();
            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEX)
            {
                throw new DatabaseException(sqlEX.Message, sqlEX.Number);
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                throw new DatabaseException(ex.Message, sqlEx.Number);
            }
        }

        public async Task HardDeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity != null)
            {
                try
                {
                    _dbSet.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEX)
                {
                    throw new DatabaseException(sqlEX.Message, sqlEX.Number);
                }
            }

            else
            {
                throw new EntityNotFoundException("The entity with the specified ID was not found.");
            }
        }
    }
}
