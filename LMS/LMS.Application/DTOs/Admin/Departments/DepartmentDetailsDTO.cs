using LMS.Application.DTOs.Admin.Employees;
using LMS.Application.DTOs.Orders;

namespace LMS.Application.DTOs.Admin.Departments
{
    public class DepartmentDetailsDTO
    {
        public string DepartmentName { get; set; }= string.Empty;
        public string Description { get; set; } = string.Empty;

        public ICollection<EmployeeOverviewDto> CurrentEmployees { get; set; } = [];
        public ICollection<EmployeeOverviewDto> FormerEmployees { get; set; } = [];
        public ICollection<OrderOverviewDto> CurrentOrders { get; set; } = [];
    }
}
