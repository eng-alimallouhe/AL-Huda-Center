using LMS.Domain.Entities.HR;
using LMS.Infrastructure.DbContexts;
using LMS.Infrastructure.Repositories.Base;

namespace LMS.Infrastructure.Repositories.HR
{
    public class LeaveBalanceRepository : SoftDeletableRepository<LeaveBalance>
    {
        private readonly LMSDbContext _context;
        public LeaveBalanceRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var leaveBalance = await _context.LeaveBalances.FindAsync(id);
            if (leaveBalance is not null)
            {
                _context.LeaveBalances.Remove(leaveBalance);
                await SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("LeaveBalance not found");
            }
        }
    }

}
