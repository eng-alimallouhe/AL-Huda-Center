using LMS.Domain.Entities.Users;
using LMS.Domain.Enums.HR;

namespace LMS.Domain.Entities.HR
{
    public class Salary
    {
        //primary key
        public Guid SalaryId { get; set; }

      
        //Foreign Key: EmployeeId ==> one(employee)-to-many(salaries) relationship
        public Guid EmployeeId { get; set; }

        
        public Month Month { get; set; }
        public decimal Value { get; set; }
        public int Year { get; set; }

        
        //Soft Delete: 
        public bool IsActive { get; set; }

        //Navigation Property:
        public Employee Employee { get; set; }

        public Salary()
        {
            SalaryId = Guid.NewGuid();
            IsActive = true;
            Employee = null!;
        }
    }
}
