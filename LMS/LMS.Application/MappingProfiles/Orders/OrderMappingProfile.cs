using AutoMapper;
using LMS.Application.DTOs.Orders;
using LMS.Domain.Entities.Orders;

namespace LMS.Application.MappingProfiles.Orders
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<BaseOrder, OrderOverviewDto>();
        }
    }
}
