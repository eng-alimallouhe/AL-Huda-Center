using LMS.Domain.Entities.Users;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Users
{

    public class NotificationRepository : BaseRepository<Notification>
    {
        public NotificationRepository(LMSDbContext context) : base(context) { }
    }
}
