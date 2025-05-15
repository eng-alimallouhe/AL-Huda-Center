using LMS.Common.Results;
using LMS.Domain.Enums.Users;
using MediatR;

namespace LMS.Application.Features.Authentication.OtpCodes.Commands.VerifyOtpCode
{
    public record VerifyOtpCodeCommand(string Email, string Code, CodeType CodeType) : IRequest<Result>;
}