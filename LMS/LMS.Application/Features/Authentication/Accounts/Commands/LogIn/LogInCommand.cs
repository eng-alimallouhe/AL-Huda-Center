using LMS.Application.DTOs.AuthenticationDTOs;
using LMS.Common.Results;
using MediatR;

namespace LMS.Application.Features.Authentication.Accounts.Commands.LogIn
{
    public record LogInCommand(string Email, string Password) : IRequest<Result<AuthorizationDTO>>;
}
