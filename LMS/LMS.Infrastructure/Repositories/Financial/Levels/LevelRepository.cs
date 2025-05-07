using LMS.Domain.Entities.Financial.Levels;
using LMS.Infrastructure.DbContexts;
using LMS.Infrastructure.Repositories.Base;

namespace LMS.Infrastructure.Repositories.Financial.Levels
{
    public class LevelRepository : SoftDeletableRepository<LoyaltyLevel>
    {
        private readonly LMSDbContext _context;
        public LevelRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var level = await _context.Levels.FindAsync(id);
            if (level != null)
            {
                level.IsActive = false;
                _context.Levels.Update(level);
                await SaveChangesAsync();
            }
            else
            {
                throw new Exception("Level not found");
            }
        }
    }
}
