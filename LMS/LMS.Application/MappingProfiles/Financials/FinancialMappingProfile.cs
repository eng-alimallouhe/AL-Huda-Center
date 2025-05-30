using AutoMapper;
using LMS.Application.DTOs.Admin.Financial;
using LMS.Application.DTOs.Admin.Sales;
using LMS.Application.DTOs.Financial;
using LMS.Domain.Entities.Financial;

namespace LMS.Application.MappingProfiles.Financials
{
    public class FinancialMappingProfile : Profile
    {
        public FinancialMappingProfile()
        {
            CreateMap<FinancialRevenue, FinanicalOverviewDto>();


            CreateMap<FinancialRevenue, FinancialDetailsDto>()
                .ForMember(dest => dest.CustomerName, 
                opt => opt.MapFrom(src => src.Customer.FullName ?? "N/A"))
                .ForMember(dest => dest.EmployeeName,
                opt => opt.MapFrom(src => src.Employee.FullName ?? "N/A"));


            CreateMap<Payment, PaymentDetailsDto>()
                .ForMember(dest => dest.EmployeeName,
                opt => opt.MapFrom(src => src.Employee.FullName ?? "N/A"));
            ;
        }
    }
}
