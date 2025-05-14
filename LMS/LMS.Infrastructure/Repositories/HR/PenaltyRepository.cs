using LMS.Domain.Entities.HR;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.HR
{
    public class PenaltyRepository : SoftDeletableRepository<Penalty>
    {
        private readonly LMSDbContext _context;
        public PenaltyRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        
        public override async Task SoftDeleteAsync(Guid id)
        {
            var penalty = await _context.Penalties.FindAsync(id);
            if (penalty is not null)
            {
                penalty.IsActive = false;
                _context.Penalties.Update(penalty);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Penalty not found");
            }
        }
    }

}
