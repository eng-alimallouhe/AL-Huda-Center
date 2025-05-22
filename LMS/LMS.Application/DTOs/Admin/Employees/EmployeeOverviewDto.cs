namespace LMS.Application.DTOs.Admin.Employees
{
    public class EmployeeOverviewDto
    {
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; }   = string.Empty;
        public DateTime HireDate { get; set; }
        public string? CurrentDepartmentName { get; set; } = string.Empty;
    }
}
