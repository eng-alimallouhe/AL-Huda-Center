using LMS.Domain.Entities.Financial.Levels;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Financial.Levels
{
    public class LevelTranslationRepository : BaseRepository<LoyaltyLevelTranslation>
    {
        public LevelTranslationRepository(LMSDbContext context) : base(context)
        {
        }
    }
}
