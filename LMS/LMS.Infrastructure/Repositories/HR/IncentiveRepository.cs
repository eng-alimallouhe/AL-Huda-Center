using LMS.Domain.Entities.HR;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.HR
{
    public class IncentiveRepository : SoftDeletableRepository<Incentive>
    {
        private readonly LMSDbContext _context;
        public IncentiveRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var incentive = await _context.Incentives.FindAsync(id);
            if (incentive is not null)
            {
                incentive.IsActive = false;
                _context.Incentives.Update(incentive);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Incentive not found");
            }
        }
    }

}
