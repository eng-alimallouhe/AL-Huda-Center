using AutoMapper;
using LMS.Application.Features.Authentication.Register.Commands.CreateTempAccount;
using LMS.Domain.Entities.Users;

namespace LMS.Application.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Customer, CreateTempAccountCommand>().ReverseMap();
        }
    }
}