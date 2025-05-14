using LMS.Common.Exceptions;
using LMS.Domain.Entities.HR;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.HR
{
    public class AttendanceRepository: SoftDeletableRepository<Attendance>
    {
        private readonly LMSDbContext _context;

        public AttendanceRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task SoftDeleteAsync(Guid id)
        {
            var attendance = await _context.Attendances.FindAsync(id);

            if (attendance is not null)
            {
                attendance.IsActive = false;
                _context.Attendances.Update(attendance);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException("Attendance not found");
            }
        }
    }

}
