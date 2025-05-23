using System.Text.Json;
using LMS.Application.Abstractions.Services.ImagesServices;
using LMS.Common.Enums;
using LMS.Common.Results;
using Microsoft.Extensions.Configuration;

namespace LMS.Infrastructure.Services.Image
{
    public class ImageAuthService : IImageAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ImageAuthService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<Result<(string, string)>> RefreshAccessTokenAsync(string refreshToken)
        {
            var clientId = _config["Imgur:ClientId"];
            var clientSecret = _config["Imgur:ClientSecret"];

            var values = new Dictionary<string, string>
            {
                { "refresh_token", refreshToken },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "grant_type", "refresh_token" }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await _httpClient.PostAsync("https://api.imgur.com/oauth2/token", content);

            if (!response.IsSuccessStatusCode)
                return Result<(string, string)>.Failure(ResponseStatus.ACTIVATION_FAILED);

            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            var access = doc.RootElement.GetProperty("access_token").GetString();
            var refresh = doc.RootElement.GetProperty("refresh_token").GetString();

            return Result<(string, string)>.Success((access!, refresh!), ResponseStatus.TASK_COMPLETED);

        }
    }
}