using AutoMapper;
using LMS.API.DTOs.Admin.Department;
using LMS.Application.Features.Admin.Departments.Command.CreateDepartment;
using LMS.Application.Features.Admin.Departments.Command.UpdateDepartment;

namespace LMS.API.MappingProfiles
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<DepartmentRequestDto, CreateDepartmentCommand>();
            CreateMap<DepartmentRequestDto, UpdateDepartmentCommand>();
        }
    }
}
