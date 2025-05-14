using LMS.Domain.Entities.Users;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Users
{
    public class OtpCodeRepository : BaseRepository<OtpCode>
    {
        public OtpCodeRepository(LMSDbContext context) : base(context) { }
    }
}
