using LMS.Domain.Entities.Users;

namespace LMS.Infrastructure.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByUserIdAsync(Guid userId);
        
        
        Task AddAsync(RefreshToken refreshToken);
        
        
        Task UpdateAsync(RefreshToken refreshToken);
        
        
        Task DeleteAsync(Guid userId);
    }
}
