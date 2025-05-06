using LMS.Domain.Enums.Commons;

namespace LMS.Domain.Entities.Financial.Levels
{
    public class LoyaltyLevelTranslation
    {
        //Promary key:
        public Guid TranslationId { get; set; }

        //Foreign key: levelId ==> one(level)--to--many(levelTranslation)
        public Guid LevelId { get; set; }

        public Language Language { get; set; }
        public string LevelName { get; set; }
        public string LevelDescription { get; set; }


        //Navigation property:
        public LoyaltyLevel Level { get; set; }


        public LoyaltyLevelTranslation()
        {
            TranslationId = Guid.NewGuid();
            LevelName = string.Empty;
            LevelDescription = string.Empty;
            Level = null!;
        }

    }
}
