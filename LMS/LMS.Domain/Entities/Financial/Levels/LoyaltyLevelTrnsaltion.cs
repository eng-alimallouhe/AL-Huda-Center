using LMS.Domain.Enums.Commons;

namespace LMS.Domain.Entities.Financial.Levels
{
    public class LoyaltyLevelTransaltion
    {
        //Primary Key:
        public Guid TranslationId { get; set; }


        //Foreign Key: LevelId ==> one(level)-to-many(translation) relationship
        public Guid LevelId { get; set; }

        public Language Language { get; set; }
        public string LevelName { get; set; } = string.Empty;
        public string LevelDescription { get; set; } = string.Empty;

        public LoyaltyLevel LoyaltyLevel { get; set; }

        public LoyaltyLevelTransaltion()
        {
            TranslationId = Guid.NewGuid();
            LoyaltyLevel = null!;
        }
    }
}
