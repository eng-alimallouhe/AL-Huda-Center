using LMS.Common.Exceptions;
using LMS.Domain.Entities.HR;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.HR
{
    public class LeaveRepository : SoftDeletableRepository<Leave>
    {
        private readonly LMSDbContext _context;
        public LeaveRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var leave = await _context.Leaves.FindAsync(id);
            if (leave is not null)
            {
                leave.IsActive = false;
                _context.Leaves.Update(leave);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException("Leave not found");
            }
        }
    }

}
