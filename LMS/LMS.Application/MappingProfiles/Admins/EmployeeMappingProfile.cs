using AutoMapper;
using LMS.Application.DTOs.Admin.Employees;
using LMS.Domain.Entities.Users;

namespace LMS.Application.MappingProfiles.Admins
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeOverviewDto>();
        }
    }
}
