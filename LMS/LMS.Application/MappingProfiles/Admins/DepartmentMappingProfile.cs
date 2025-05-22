using AutoMapper;
using LMS.Application.DTOs.Admin.Departments;
using LMS.Application.Features.Admin.Departments.Command.CreateDepartment;
using LMS.Domain.Entities.Users;

namespace LMS.Application.MappingProfiles.Admins
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<Department, DepartmentOverviewDto>()
                    .ForMember(dest => dest.EmployeesCount, otp => otp.MapFrom(src => src.EmployeeDepartments.Count(em => em.IsActive)));

            CreateMap<Department, DepartmentDetailsDTO>();
            
            CreateMap<CreateDepartmentCommand, Department>();
        }
    }
}
