using AutoMapper;
using LMS.Application.DTOs.Financial;
using LMS.Application.DTOs.Orders;
using LMS.Application.DTOs.Users;
using LMS.Application.Features.Authentication.Register.Commands.CreateTempAccount;
using LMS.Domain.Entities.Users;

namespace LMS.Application.MappingProfiles.Users
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Customer, CreateTempAccountCommand>();

            CreateMap<Customer, CustomersOverViewDto>()
                .ForMember(dest => dest.TotalAmountSpent, 
                opt => opt.MapFrom(src => src.Orders.Sum(fr => fr.Cost)));


            CreateMap<Customer, InActiveCustomersDto>()
                .ConvertUsing<CustomerToInActiveCustomer>();

            CreateMap<Customer, CustomerDetailsDto>()
                .ConvertUsing<CustomerToInOverViewConverter>();
        }


        //Mapping Configurations: 
        public class CustomerToInActiveCustomer : ITypeConverter<Customer, InActiveCustomersDto>
        {
            public InActiveCustomersDto Convert(Customer source, InActiveCustomersDto destination, ResolutionContext context)
            {
                var order = source.Orders.OrderByDescending(order => order.CreatedAt).FirstOrDefault();

                DateTime lastOrderDate = DateTime.MinValue;

                if (order is not null)
                {
                    lastOrderDate = order.CreatedAt;
                }


                return new InActiveCustomersDto
                {
                    FullName = source.FullName,
                    Email = source.Email,
                    CreatedAt = source.CreatedAt,
                    LastOrderDate = lastOrderDate
                };
            }
        }

        public class CustomerToInOverViewConverter : ITypeConverter<Customer, CustomerDetailsDto>
        {
            public CustomerDetailsDto Convert(Customer source, CustomerDetailsDto destination, ResolutionContext context)
            {
                var dto = new CustomerDetailsDto
                {
                    UserId = source.UserId,
                    FullName = source.FullName,
                    Email = source.Email,
                    UserName = source.UserName,
                    PhoneNumber = source.PhoneNumber,
                    CreatedAt = source.CreatedAt,
                    UpdatedAt = source.UpdatedAt,
                    LastLogIn = source.LastLogIn,

                    ViewOrders = source.Orders.Select(o => new OrderOverviewDto
                    {
                        OrderId = o.OrderId,
                        Status = o.Status,
                        CreatedAt = o.CreatedAt,
                        UpdatedAt= o.UpdatedAt,
                        Cost = o.Cost,
                    }).ToList(),

                    ViewFinancial = source.FinancialRevenues.Select(fr => new FinanicalOverviewDto
                    {
                        FinancialRevenueId = fr.FinancialRevenueId,
                        Amount = fr.Amount,
                        Date = fr.CreatedAt
                    }).ToList()
                };

                throw new NotImplementedException();
            }
        }
    }
}