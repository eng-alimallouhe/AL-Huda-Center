using LMS.Domain.Entities.Users;

namespace LMS.Domain.Entities.Financial
{
    public class Payment
    {
        //primary key: 
        public Guid PaymentId { get; set; }

        public Guid EmployeeId { get; set; }

        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Reasone { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;

        
        //soft delete: 
        public bool IsActive { get; set; }

        public Employee Employee { get; set; }

        public Payment()
        {
            PaymentId = Guid.NewGuid();
            IsActive = true;
            Employee = null!;
        }
    }
}
