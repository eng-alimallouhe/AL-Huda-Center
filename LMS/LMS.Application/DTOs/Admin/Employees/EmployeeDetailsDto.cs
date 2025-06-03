using LMS.Application.DTOs.Admin.Departments;
using LMS.Application.DTOs.Admin.HR;
using LMS.Application.DTOs.Financial;

namespace LMS.Application.DTOs.Admin.Employees
{
    public class EmployeeDetailsDto
    {
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? CurrentDepartmentName { get; set; } = string.Empty;

        public ICollection<FinanicalOverviewDto> EmployeeFinanical { get; set; } = [];
        public ICollection<DepartmentHistoryDto> DepartmentsHistory { get; set; } = [];
        public ICollection<AttendanceOverviewDto> AttendancesView { get; set; } = [];
        public ICollection<IncentivesOverViewDto>  IncentivesView { get; set; } = [];
        public ICollection<PenaltiesOverviewDto>  PenaltiesView { get; set; } = [];
        public ICollection<LeavesOverViewDto>  LeavesView { get; set; } = [];
        public ICollection<SalaiesOverviewDto>  SalariesView { get; set; } = [];
    }
}
