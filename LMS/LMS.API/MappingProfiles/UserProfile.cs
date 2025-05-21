using AutoMapper;
using LMS.API.DTOs.Authentication;
using LMS.Application.Features.Authentication.Accounts.Commands.LogIn;
using LMS.Application.Features.Authentication.OtpCodes.Commands.SendOtpCode;
using LMS.Application.Features.Authentication.OtpCodes.Commands.VerifyOtpCode;
using LMS.Application.Features.Authentication.Register.Commands.CreateTempAccount;
using LMS.Domain.Enums.Users;

namespace LMS.API.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterRequestDTO, CreateTempAccountCommand>();
            CreateMap<OtpCodeSendRequstDTO, SendOtpCodeCommand>()
                .ForMember(dest => dest.CodeType, opt => opt.MapFrom(src => (CodeType) src.CodeType));
            CreateMap<OtpVerifyRequest, VerifyOtpCodeCommand>();
            CreateMap<LoginRequestDTO, LogInCommand>();
        }
    }
}
