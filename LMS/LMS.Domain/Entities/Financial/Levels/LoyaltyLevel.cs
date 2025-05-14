using LMS.Domain.Entities.Users;

namespace LMS.Domain.Entities.Financial.Levels
{
    public class LoyaltyLevel
    {
        //Primary Key:
        public Guid LevelId { get; set; }


        public int RequiredPoints { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal PointPerDolar { get; set; }

        //soft delete: 
        public bool IsActive { get; set; }

        //Timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //navigation property:
        public ICollection<Customer> Customers { get; set; }
        public ICollection<LoyaltyLevelTransaltion> Translations { get; set; }

        public LoyaltyLevel()
        {
            LevelId = Guid.NewGuid();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Customers =  [];
            Translations = [];
        }
    }
}
