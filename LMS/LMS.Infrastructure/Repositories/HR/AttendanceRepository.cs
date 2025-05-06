using LMS.Domain.Entities.HR;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.HR
{
    public class AttendanceRepository: BaseRepository<Attendance>
    {
        public AttendanceRepository(LMSDbContext context) : base(context)
        {
        }
    }

}
