using LMS.Domain.Entities.Financial;
using LMS.Domain.Entities.Financial.Levels;
using LMS.Domain.Entities.Orders;

namespace LMS.Domain.Entities.Users
{
    public class Customer : User
    {
        //Foreign Key: LevelId ==> one(customer)-to-one(level) relationship
        public Guid LevelId { get; set; }


        public decimal Points { get; set; }


        //navigation property:
        public LoyaltyLevel Level { get; set; }
        public ICollection<Address> Addresses { get; set; } 
        public Cart Cart { get; set; } 
        public ICollection<BaseOrder> Orders { get; set; } 
        public ICollection<FinancialRevenue> FinancialRevenues { get; set; }


        public Customer()
        {
            LevelId = Guid.NewGuid();
            Level = null!;
            Addresses = [];
            Cart = null!;
            Orders = null!;
            FinancialRevenues = null!;
        }

    }
}
