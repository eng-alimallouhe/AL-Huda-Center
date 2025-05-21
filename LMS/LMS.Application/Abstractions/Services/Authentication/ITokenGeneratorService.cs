using LMS.Application.DTOs.AuthenticationDTOs;
using LMS.Common.Results;

namespace LMS.Application.Abstractions.Services.Authentication
{
    public interface ITokenGeneratorService
    {
        Task<Result<string>> GenerateAccessTokenAsync(Guid userId);
        Task<Result<string>> GenerateRefreshTokenAsync(Guid userId);

        Task<Result<AuthorizationDTO>> ValidateTokenAsync(string refreshToken, string accessToken);
    }
}
