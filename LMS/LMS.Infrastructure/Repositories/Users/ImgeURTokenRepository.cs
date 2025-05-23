using LMS.Domain.Entities.HttpEntities;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Users
{
    public class ImgeURTokenRepository : BaseRepository<ImgeURToken>
    {
        public ImgeURTokenRepository(LMSDbContext context) : base(context) { }
    }
}
