using AutoMapper;
using LMS.Application.DTOs.Financial;
using LMS.Domain.Entities.Financial;

namespace LMS.Application.MappingProfiles.Financials
{
    public class FinancialMappingProfile : Profile
    {
        public FinancialMappingProfile()
        {
            CreateMap<FinancialRevenue, FinanicalOverviewDto>();
        }
    }
}
