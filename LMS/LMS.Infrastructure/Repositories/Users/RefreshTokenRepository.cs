using LMS.Domain.Entities.Users;
using LMS.Infrastructure.DbContexts;
using LMS.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories.Users
{
    public class RefreshTokenRepository: IRefreshTokenRepository
    {
        private readonly LMSDbContext _context;
            
        public RefreshTokenRepository(LMSDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetByUserIdAsync(Guid userId)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.UserId == userId);
        }

        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid userId)
        {
            var refreshToken = await GetByUserIdAsync(userId);
            
            if (refreshToken != null)
            {
                _context.RefreshTokens.Remove(refreshToken);
                await _context.SaveChangesAsync();
                return;
            }
            throw new Exception();
        }
    }
}
