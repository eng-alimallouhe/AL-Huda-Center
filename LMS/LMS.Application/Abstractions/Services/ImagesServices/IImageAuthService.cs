using LMS.Common.Results;

namespace LMS.Application.Abstractions.Services.ImagesServices
{
    public interface IImageAuthService
    {
        Task<Result<(string AccessToken, string RefreshToken)>> RefreshAccessTokenAsync(string refreshToken);
    }
}
