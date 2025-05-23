using AutoMapper;
using LMS.API.DTOs.Admin.Employee;
using LMS.Application.Features.Admin.Departments.Command.UpdateDepartment;
using LMS.Application.Features.Admin.Employees.Command.CreateDepartment;
using LMS.Application.Features.Admin.Employees.Command.TransferEmployee;
using LMS.Domain.Enums.Commons;

namespace LMS.API.MappingProfiles
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<EmployeeCreateRequestDto, CreateEmployeeCommand>()
                .ForMember(dest => dest.Language, 
                otp => otp.MapFrom(src => (Language) src.Language))
                .ForMember(dest => dest.ProfilePictureUrl, 
                opt => opt.MapFrom(src => " "));


            CreateMap<EmployeeUpdateRequestDto, UpdateDepartmentCommand>()
                .ForMember(dest => dest.DepartmentId, 
                opt => opt.MapFrom(src => Guid.NewGuid()));


            CreateMap<TransferEmployeeRequestDto, TransferEmployeeCommand>()
                .ForMember(dest => dest.AppointmentDecisionUrl, 
                opt => opt.MapFrom(src => " "));
        }
    }
}
