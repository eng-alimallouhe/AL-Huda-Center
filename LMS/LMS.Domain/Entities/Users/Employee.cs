using LMS.Domain.Entities.Financial;
using LMS.Domain.Entities.HR;
using LMS.Domain.Entities.Orders;

namespace LMS.Domain.Entities.Users
{
    public class Employee : User
    {
        public DateTime HireDate { get; set; }
        public decimal BaseSalary { get; set; }


        //Navigation Property:
        public ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }
        public ICollection<Attendance> Attendances { get; set; }  
        public ICollection<Incentive> Incentives { get; set; }  
        public ICollection<Penalty> Penalties { get; set; }  
        public ICollection<Leave> Leaves { get; set; }  
        public ICollection<Salary> Salaries { get; set; }  
        public LeaveBalance LeaveBalance { get; set; }
        public ICollection<BaseOrder> Orders { get; set; }  
        public ICollection<FinancialRevenue> FinancialRevenues { get; set; }


        public Employee()
        {
            EmployeeDepartments = [];
            Attendances = [];
            Incentives = [];
            Penalties = [];
            Leaves = [];
            Salaries = [];
            LeaveBalance = null!;
            Orders = [];
            FinancialRevenues = [];
        }
    }
}