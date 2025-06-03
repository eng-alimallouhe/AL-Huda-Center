using AutoMapper;
using LMS.Application.DTOs.Admin.Departments;
using LMS.Application.DTOs.Admin.Employees;
using LMS.Application.DTOs.Financial;
using LMS.Domain.Entities.Users;

namespace LMS.Application.MappingProfiles.Users.MappingsSettings
{
    public class EmployeeToDetailsConverter : ITypeConverter<Employee, EmployeeDetailsDto>
    {
        public EmployeeDetailsDto Convert(Employee source, EmployeeDetailsDto destination, ResolutionContext context)
        {
            var dto = new EmployeeDetailsDto
            {
                EmployeeId = source.UserId,
                FullName = source.FullName,
                HireDate = source.HireDate,

                CurrentDepartmentName = source.EmployeeDepartments?
                    .FirstOrDefault(ed => ed.IsActive)?.Department?.DepartmentName ?? "N/A",

                DepartmentsHistory = source.EmployeeDepartments.Select(ed => new DepartmentHistoryDto
                {
                    DepartmentName = ed.Department?.DepartmentName ?? "N/A",
                    JoinDate = ed.StartDate,
                    IsCurrent = ed.IsActive
                }).ToList(),

                EmployeeFinanical = source.FinancialRevenues.Select(ed => new FinanicalOverviewDto
                {
                    FinancialRevenueId = ed.FinancialRevenueId,
                    Amount = ed.Amount,
                    Date = ed.CreatedAt
                }).ToList(),
            };

            return dto;
        }
    }
}
