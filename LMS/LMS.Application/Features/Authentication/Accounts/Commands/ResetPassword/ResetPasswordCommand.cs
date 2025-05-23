using LMS.Application.DTOs.AuthenticationDTOs;
using LMS.Common.Results;
using MediatR;

namespace LMS.Application.Features.Authentication.Accounts.Commands.ResetPassword
{
    public record ResetPasswordCommand(
        string Email,
        string NewPassword) : IRequest<Result<AuthorizationDTO>>;
}
