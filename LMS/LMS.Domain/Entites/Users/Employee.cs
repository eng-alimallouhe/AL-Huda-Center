using LMS.Domain.Entites.HR;
using LMS.Domain.Entites.Orders;
using LMS.Domain.Entities.Financial;

namespace LMS.Domain.Entites.Users
{
    public class Employee : User
    {
        public DateTime HireDate { get; set; }
        public decimal BaseSalary { get; set; }


        //navigation property:
        public ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }
        public ICollection<Attendance> Attendances { get; set; } 
        public ICollection<Incentive> Incentives { get; set; }
        public ICollection<Penalty> Penalties { get; set; }
        public ICollection<Leave> Leaves { get; set; }
        public ICollection<Salary> Salaries { get; set; }
        public ICollection<BaseOrder> Orders { get; set; }
        public ICollection<Shipment> Shipments { get; set; }
        public ICollection<FinancialRevenue> Revenues { get; set; }
        public LeaveBalance LeaveBalance { get; set; }

        public Employee()
        {
            EmployeeDepartments = [];
            Attendances = [];
            Incentives = [];
            Penalties = [];
            Leaves = [];
            Salaries = [];
            Orders = [];
            Shipments = [];
            Revenues = [];
            LeaveBalance = null!;
        }
    }
}
