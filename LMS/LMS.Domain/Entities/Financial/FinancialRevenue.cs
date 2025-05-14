using LMS.Domain.Entities.Users;
using LMS.Domain.Enums.Finacial;
using LMS.Domain.Enums.Orders;

namespace LMS.Domain.Entities.Financial
{
    public class FinancialRevenue
    {
        //Primary Key:
        public Guid FinancialRevenueId { get; set; }

        //Foreign Key: CustomerId ==> one(Customer)-to-many(Payment) relationship
        public Guid CustomerId { get; set; }

        //Foreign Key: EmployeeId ==> one(Employee)-to-many(PrintOrder) relationship
        public Guid EmployeeId { get; set; }

        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public Service Service { get; set; }
        
        //Soft Delete:
        public bool IsActive { get; set; }

        //Timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //Navigation Property: 
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }

        public FinancialRevenue()
        {
            FinancialRevenueId = Guid.NewGuid();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Customer = null!;
            Employee = null!;
        }
    }
}
