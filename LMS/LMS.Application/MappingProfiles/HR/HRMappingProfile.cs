using AutoMapper;
using LMS.Application.DTOs.Admin.HR;
using LMS.Application.MappingProfiles.HR.MappingSettings;
using LMS.Domain.Entities.HR;

namespace LMS.Application.MappingProfiles.HR
{
    public class HRMappingProfile : Profile
    {
        public HRMappingProfile()
        {

            //Attendances Mappings:
            CreateMap<Attendance, AttendanceOverviewDto>()
                .ConvertUsing<AttendanceToOverviewConverter>();


            //Penalties Mappings:
            CreateMap<Penalty, PenaltiesOverviewDto>()
                .ForMember(dest => dest.Date, 
                opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.IsDeducted,
                opt => opt.MapFrom(src => (src.IsDeducted)? "Yes" : "No"));


            //Incentives Mappings:
            CreateMap<Incentive, IncentivesOverViewDto>()
                .ForMember(dest => dest.Date,
                opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.IsDisbursed,
                opt => opt.MapFrom(src =>  (src.IsDisbursed)? "Yes" : "No"));



            //Leaves Mappings:
            CreateMap<Leave, LeavesOverViewDto>()
                .ForMember(dest => dest.StartDate, 
                opt => opt.MapFrom(src => src.StartDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.EndDate, 
                opt => opt.MapFrom(src => src.EndDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.LeaveType, 
                opt => opt.MapFrom(src => src.LeaveType.ToString()))
                .ForMember(dest => dest.IsAbroved, 
                opt => opt.MapFrom(src => src.IsAproved ? "Yes" : "No"));


            //Salaries Mappings:
            CreateMap<Salary, SalaiesOverviewDto>()
                .ForMember(dest => dest.Date, 
                opt => opt.MapFrom(src => src.Year + " - " + src.Month.ToString()));
        }
    }
}
