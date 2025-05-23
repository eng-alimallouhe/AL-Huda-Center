using AutoMapper;
using LMS.API.Controllers.Authintication;
using LMS.API.DTOs.Authentication;
using LMS.Application.Features.Authentication.Accounts.Commands.LogIn;
using LMS.Application.Features.Authentication.Accounts.Commands.ResetPassword;
using LMS.Application.Features.Authentication.OtpCodes.Commands.SendOtpCode;
using LMS.Application.Features.Authentication.OtpCodes.Commands.VerifyOtpCode;
using LMS.Application.Features.Authentication.Register.Commands.CreateTempAccount;
using LMS.Application.Features.Authentication.Tokens.Command.AuthenticationRefresh;
using LMS.Domain.Enums.Users;

namespace LMS.API.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterRequestDTO, CreateTempAccountCommand>()
                .ForMember(dest => dest.ProfilePictureUrl, 
                opt => opt.MapFrom(
                    src => " "
                    ));
            
            CreateMap<OtpCodeSendRequstDTO, SendOtpCodeCommand>()
                .ForMember(dest => dest.CodeType, opt => opt.MapFrom(src => (CodeType) src.CodeType));
            
            CreateMap<OtpVerifyRequest, VerifyOtpCodeCommand>();
            
            CreateMap<LoginRequestDTO, LogInCommand>();

            CreateMap<ResetPasswordDto, ResetPasswordCommand>();

            CreateMap<AuthorizationRequestDTO, AuthenticationRefreshCommand>();
        }
    }
}
