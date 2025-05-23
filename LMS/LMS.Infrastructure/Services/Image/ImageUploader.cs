using System.Net.Http.Headers;
using System.Text.Json;
using LMS.Application.Abstractions.Services.ImagesServices;
using LMS.Common.Enums;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.HttpEntities;
using Microsoft.Extensions.Configuration;


namespace LMS.Infrastructure.Services.Image
{
    public class ImageUploader : IImageUploader
    {
        private readonly HttpClient _httpClient;
        private readonly IImageAuthService _authService;
        private readonly IBaseRepository<ImgeURToken> _tokenRepo;

        public ImageUploader(
            HttpClient httpClient,
            IImageAuthService authService,
            IBaseRepository<ImgeURToken> tokenRepo)
        {
            _httpClient = httpClient;
            _authService = authService;
            _tokenRepo = tokenRepo;
        }

        public async Task<Result<string>> UploadImageAsync(Stream imageStream, string fileName)
        {
            var tokenInfo = await _tokenRepo.GetBySpecificationAsync(new Specification<ImgeURToken>());

            var result = await TryUploadAsync(imageStream, fileName, tokenInfo!.AccessToken);

            if (result.Status == ResponseStatus.ACTIVATION_FAILED)
            {
                var refreshResult = await _authService.RefreshAccessTokenAsync(tokenInfo.RefreshToken);
                if (!refreshResult.IsSuccess) return Result<string>.Failure(refreshResult.Status);

                var (newAccess, newRefresh) = refreshResult.Value!;
                tokenInfo.AccessToken = newAccess;
                tokenInfo.RefreshToken = newRefresh;
                await _tokenRepo.UpdateAsync(tokenInfo);

                result = await TryUploadAsync(imageStream, fileName, newAccess);
            }

            return result;
        }


        private async Task<Result<string>> TryUploadAsync(Stream imageStream, string fileName, string accessToken)
        {
            try
            {
                imageStream.Position = 0;
                
                using var content = new MultipartFormDataContent();
                
                using var imageContent = new StreamContent(imageStream);
                
                imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                
                content.Add(imageContent, "image", fileName);

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.PostAsync("https://api.imgur.com/3/image", content);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return Result<string>.Failure(ResponseStatus.ACTIVATION_FAILED);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(responseString);
                var link = json.RootElement.GetProperty("data").GetProperty("link").GetString();

                return Result<string>.Success(link!, ResponseStatus.TASK_COMPLETED);
            }
            catch
            {
                return Result<string>.Failure(ResponseStatus.HTTP_RESPONSE_ERROR);
            }
        }
    }

}