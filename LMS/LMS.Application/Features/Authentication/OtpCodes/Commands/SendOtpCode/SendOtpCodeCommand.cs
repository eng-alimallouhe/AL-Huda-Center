using LMS.Common.Enums;
using LMS.Common.Results;
using LMS.Domain.Enums.Users;
using MediatR;

namespace LMS.Application.Features.Authentication.OtpCodes.Commands.SendOtpCode
{
    public record SendOtpCodeCommand(string Email, SupportedLanguages Language,  CodeType CodeType) : IRequest<Result>;
}