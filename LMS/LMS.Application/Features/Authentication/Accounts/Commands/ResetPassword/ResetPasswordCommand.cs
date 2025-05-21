using LMS.Application.DTOs.AuthenticationDTOs;
using LMS.Common.Results;
using MediatR;

namespace LMS.Application.Features.Authentication.Accounts.Commands.ResetPassword
{
    public record ResetPasswordCommand(string email) : IRequest<Result<AuthorizationDTO>>;
}
