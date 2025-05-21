using LMS.Application.DTOs.AuthenticationDTOs;
using LMS.Application.Features.Authentication.Register.Commands.CreateTempAccount;
using LMS.Common.Results;
using LMS.Domain.Entities.Users;
using LMS.Domain.Enums.Users;

namespace LMS.Application.Abstractions.Services.Authentication
{
    public interface IAuthenticationHelper
    {
        Task<Result<string>> GenerateAndSaveCodeAsync(Guid userId, CodeType codeType);

        Task<Result> VerifyCodeAsync(Guid userId, string code, CodeType codeType);

        Task<Result> CreateAndSaveAccountAsync(CreateTempAccountCommand request);

        Task<Result> LogIn(User user, string password);

        Task<Result<Guid>> CanActivateAuth(string email, CodeType activationType);
    }
}
