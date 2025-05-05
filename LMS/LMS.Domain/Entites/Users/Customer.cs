using LMS.Domain.Entites.Orders;
using LMS.Domain.Entities.Financial;

namespace LMS.Domain.Entites.Users
{
    public class Customer : User
    {
        //Foreign Key: LevelId ==> one(customer)-to-one(level) relationship
        public Guid LevelId { get; set; }


        public decimal Points { get; set; }


        //Navigation Property:
        public LoyaltyLevel Level { get; set; }
        public ICollection<BaseOrder> Orders { get; set; }
        public ICollection<FinancialRevenue> Revenues { get; set; }
        public ICollection<Shipment> Shipments { get; set; }

        public Customer()
        {
            Level = null!;
            Revenues = [];
            Shipments = [];
            Orders = [];
        }
    }
}
