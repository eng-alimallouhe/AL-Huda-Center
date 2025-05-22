namespace LMS.Application.DTOs.Admin.Departments
{
    public class DepartmentHistoryDto
    {
        public string DepartmentName { get; set; } = string.Empty;
        public DateTime JoinDate { get; set; }
        public bool IsCurrent { get; set; }
    }
}
