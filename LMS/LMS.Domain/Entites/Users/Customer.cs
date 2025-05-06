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


        //Navigation Property:
        public Cart Cart { get; set; }
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
