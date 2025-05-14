using LMS.Domain.Entities.Financial.Levels;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Financial
{
    public class LevelTranslationRepository : BaseRepository<LoyaltyLevelTransaltion>
    {
        public LevelTranslationRepository(LMSDbContext context) : base(context) { }
    }
}
