using LMS.Common.Results;
using LMS.Domain.Enums.Users;
using MediatR;

namespace LMS.Application.Features.Authentication.OtpCodes.Commands.SendOtpCode
{
    public record SendOtpCodeCommand(string Email,  CodeType CodeType) : IRequest<Result>;
}