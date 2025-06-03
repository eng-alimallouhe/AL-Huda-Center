using AutoMapper;
using LMS.Application.DTOs.Admin.Employees;
using LMS.Domain.Entities.Users;

namespace LMS.Application.MappingProfiles.Users.MappingsSettings
{
    //Mapping Configurations:
    public class EmployeeToOverviewConverter : ITypeConverter<Employee, EmployeeOverviewDto>
    {
        public EmployeeOverviewDto Convert(Employee source, EmployeeOverviewDto destination, ResolutionContext context)
        {
            return new EmployeeOverviewDto
            {
                EmployeeId = source.UserId,
                FullName = source.FullName,
                HireDate = source.HireDate,
                CurrentDepartmentName = source.EmployeeDepartments
                    .FirstOrDefault(ed => ed.IsActive)?.Department?.DepartmentName ?? "N/A"
            };
        }
    }
}
