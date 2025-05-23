using AutoMapper;
using LMS.Application.DTOs.Admin.Departments;
using LMS.Application.DTOs.Admin.Employees;
using LMS.Application.DTOs.Financial;
using LMS.Application.Features.Admin.Departments.Command.CreateDepartment;
using LMS.Application.Features.Admin.Employees.Command.CreateDepartment;
using LMS.Application.Features.Admin.Employees.Command.TransferEmployee;
using LMS.Application.Features.Admin.Employees.Command.UpdateEmployee;
using LMS.Domain.Entities.Users;

namespace LMS.Application.MappingProfiles.Admins
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeOverviewDto>()
                .ConvertUsing<EmployeeToOverviewConverter>();

            CreateMap<Employee, EmployeeDetailsDto>()
                .ConvertUsing<EmployeeToDetailsConverter>();

            CreateMap<Employee, EmployeeDetailsDto>();

            CreateMap<CreateEmployeeCommand, Employee>();

            CreateMap<UpdateEmployeeCommand, Employee>().ReverseMap();

            CreateMap<TransferEmployeeCommand, EmployeeDepartment>();
        }
    }

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

    public class EmployeeToDetailsConverter : ITypeConverter<Employee, EmployeeDetailsDto>
    {
        public EmployeeDetailsDto Convert(Employee source, EmployeeDetailsDto destination, ResolutionContext context)
        {
            var dto = new EmployeeDetailsDto
            {
                EmployeeId = source.UserId,
                FullName = source.FullName,
                HireDate = source.HireDate,

                CurrentDepartmentName = source.EmployeeDepartments
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
                }).ToList()
            };

            return dto;
        }
    }
}
