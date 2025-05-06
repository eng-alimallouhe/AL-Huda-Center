using System.Linq.Expressions;
using LMS.Domain.Entities.Users;
using LMS.Domain.Interfaces;
using LMS.Infrastructure.DbContexts;
using LMS.Infrastructure.Interfaces;
using LMS.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories.Users
{
    public class UserRepositroy : IUserRepository
    {
        private readonly LMSDbContext _context;

        
        public UserRepositroy(LMSDbContext context)
        {
            _context = context;
        }
       
        
        public async Task<ICollection<User>> GetAllAsync(ISpecification<User> specification)
        {
            var query = SpecificationQueryBuilder.GetQuery<User>(_context.Users, specification);
            return await query.ToListAsync();
        }
        
        public async Task<User?> GetBySpecificationAsync(ISpecification<User> specification)
        {
            var query = SpecificationQueryBuilder.GetQuery<User>(_context.Users, specification);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<User?> GetByExpressionAsync(Expression<Func<User, bool>> expression)
        {
            var result = await _context.Users.FirstOrDefaultAsync(expression); 
            return result;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> AddAsync(User entity)
        {
            try
            {
                await _context.Users.AddAsync(entity);
                await SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task UpdateAsync(User entity)
        {
            try
            {
                _context.Users.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task SoftDeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.IsBlocked = true;
                await _context.SaveChangesAsync();
                return;
            }
            throw new KeyNotFoundException("user not found");
        }

        
        public async Task HardDeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return;
            }
            throw new KeyNotFoundException("user not found");
        }

        
        public async Task LockUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.IsLocked = true;
                await _context.SaveChangesAsync();
                return;
            }
            throw new KeyNotFoundException("user not found");
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
