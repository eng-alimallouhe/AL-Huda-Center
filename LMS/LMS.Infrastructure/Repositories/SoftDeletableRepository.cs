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
    public abstract class SoftDeletableRepository<TEntity> 
        : ISoftDeletableRepository<TEntity> 
        where TEntity : class
    {
        private readonly LMSDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public SoftDeletableRepository(LMSDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        
        public async Task<(ICollection<TEntity> items, int count)> GetAllAsync(ISpecification<TEntity> specification)
        {
            var count = SpecificationQueryBuilder.GetQuery(_dbSet, specification, false).Count();
            var query = SpecificationQueryBuilder.GetQuery(_dbSet, specification, true);
            
            return (await query.ToListAsync(), count);
        }


        public async Task<ICollection<TResult>> GetAllProjectedAsync<TResult, TKey>(IProjectedSpecification<TEntity, TResult, TKey> specification)
        {
            var query = SpecificationQueryBuilder.GetQuery(_dbSet, specification);
            Console.WriteLine(specification.Take);
            Console.WriteLine(specification.Skip);
            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetBySpecificationAsync(ISpecification<TEntity> specification)
        {
            var query = SpecificationQueryBuilder.GetQuery(_dbSet, specification, true);
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

        public IQueryable<TEntity> AsQueryable()
        {
            _dbSet.AsQueryable();
            return _dbSet.AsQueryable();
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
                throw new DatabaseException(sqlEX.Message , sqlEX.Number);
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
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }

            else
            {
                throw new EntityNotFoundException("The entity with the specified ID was not found.");
            }
        }

        public abstract Task SoftDeleteAsync(Guid id);
    }
}
