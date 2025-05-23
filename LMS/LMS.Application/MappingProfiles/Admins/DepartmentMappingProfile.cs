using AutoMapper;
using LMS.Application.DTOs.Admin.Departments;
using LMS.Application.Features.Admin.Departments.Command.CreateDepartment;
using LMS.Application.Features.Admin.Departments.Command.UpdateDepartment;
using LMS.Domain.Entities.Users;

namespace LMS.Application.MappingProfiles.Admins
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<Department, DepartmentOverviewDto>()
                    .ForMember(dest => dest.DepartmentDescription, 
                    opt => opt.MapFrom(src => src.DepartmentDescription))

                    .ForMember(dest => dest.EmployeesCount, 
                    otp => otp.MapFrom(src => src.EmployeeDepartments.Count(em => em.IsActive)));

            CreateMap<Department, DepartmentDetailsDTO>();
            
            CreateMap<CreateDepartmentCommand, Department>();
            
            CreateMap<UpdateDepartmentCommand, Department>();
        }
    }
}
