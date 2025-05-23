using LMS.Common.Results;
using MediatR;

namespace LMS.Application.Features.Authentication.Register.Commands.CreateTempAccount
{
    public record CreateTempAccountCommand(
        string FullName, 
        string UserName, 
        string PhoneNumber,
        string Email, 
        string Password,
        int Language,
        string? ProfilePictureUrl = " ") : IRequest<Result>;
}