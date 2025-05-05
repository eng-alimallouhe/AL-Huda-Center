namespace LMS.Domain.Entites.Users
{
    public class Employee : User
    {
        public DateTime HireDate { get; set; }
        public decimal BaseSalary { get; set; }


        //navigation property:
        public ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }

        public Employee()
        {
            EmployeeDepartments = [];
        }
    }
}
