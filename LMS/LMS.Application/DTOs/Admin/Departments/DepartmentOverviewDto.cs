namespace LMS.Application.DTOs.Admin.Departments
{
    public class DepartmentOverviewDto
    {
        public string DepartmentName { get; set; } = string.Empty;
        public string DepartmentDescription { get; set; } = string.Empty;
        public int EmployeesCount { get; set; }
    }
}
