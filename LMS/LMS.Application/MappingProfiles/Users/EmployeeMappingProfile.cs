using AutoMapper;
using LMS.Application.DTOs.Admin.Employees;
using LMS.Application.Features.Admin.Employees.Command.CreateDepartment;
using LMS.Application.Features.Admin.Employees.Command.TransferEmployee;
using LMS.Application.Features.Admin.Employees.Command.UpdateEmployee;
using LMS.Application.MappingProfiles.Users.MappingsSettings;
using LMS.Domain.Entities.Users;

namespace LMS.Application.MappingProfiles.Users
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeOverviewDto>()
                .ConvertUsing<EmployeeToOverviewConverter>();

            CreateMap<Employee, EmployeeDetailsDto>()
                .ConvertUsing<EmployeeToDetailsConverter>();

            CreateMap<CreateEmployeeCommand, Employee>();

            CreateMap<UpdateEmployeeCommand, Employee>();

            CreateMap<TransferEmployeeCommand, EmployeeDepartment>();
        }
    }
}
