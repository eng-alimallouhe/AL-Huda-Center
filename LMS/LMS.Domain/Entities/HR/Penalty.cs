using LMS.Domain.Entities.Users;

namespace LMS.Domain.Entities.HR
{
    public class Penalty
    {
        //primary key
        public Guid PenaltyId { get; set; }

        
        //Foreign Key: EmployeeId ==> one(employee)-to-many(penalties) relationship
        public Guid EmployeeId { get; set; }

        
        public decimal Amount { get; set; }
        public string DecisionFileUrl { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        
        //Soft delete
        public bool IsActive { get; set; } 

        
        //Navigation Property:
        public Employee Employee { get; set; }


        public Penalty()
        {
            PenaltyId = Guid.NewGuid();
            IsActive = true;
            Employee = null!;
        }
    }
}
