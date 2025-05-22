namespace LMS.Application.DTOs.Admin.Employees
{
    public class EmployeeOverviewDto
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }   = string.Empty;
        public DateTime HireDate { get; set; }
    }
}
