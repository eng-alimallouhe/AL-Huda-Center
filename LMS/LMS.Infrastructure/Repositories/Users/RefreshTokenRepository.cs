using LMS.Domain.Entities.Users;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Users
{
    public class RefreshTokenRepository: BaseRepository<RefreshToken>
    {
        public RefreshTokenRepository(LMSDbContext context) : base(context) { }
    }
}
