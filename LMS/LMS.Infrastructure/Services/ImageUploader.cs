using System.Net.Http.Headers;
using System.Text.Json;
using LMS.Common.Enums;
using LMS.Common.Interfaces;
using LMS.Common.Results;
using Microsoft.Extensions.Configuration;

namespace LMS.Infrastructure.Services
{
    public class ImageUploader : IImageUploader
    {
        private readonly HttpClient _httpClient;
        private readonly string? _clientId;


        public ImageUploader(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _clientId = configuration["Imgur:ClientId"] ?? null;
        }

        public async Task<Result<string>> UploadImageAsync(Stream imageStream, string fileName)
        {
            try
            {
                if (imageStream == null || !imageStream.CanRead || string.IsNullOrWhiteSpace(fileName))
                    return Result<string>.Failure(ResponseStatus.FILE_NOT_FOUND);


                using var content = new MultipartFormDataContent();
                using var imageContent = new StreamContent(imageStream);

                string contentType = GetContentType(fileName);
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
                content.Add(imageContent, "image", fileName);


                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Client-ID", _clientId);


                var response = await _httpClient.PostAsync("https://api.imgur.com/3/image", content);
                response.EnsureSuccessStatusCode();


                var responseString = await response.Content.ReadAsStringAsync();


                using var json = JsonDocument.Parse(responseString);
                if (json.RootElement.TryGetProperty("data", out var data) &&
                    data.TryGetProperty("link", out var link))
                {
                    return Result<string>.Success(link.GetString()!, ResponseStatus.TASK_COMPLETED);
                }

                return Result<string>.Failure(ResponseStatus.HTTP_RESPONSE_ERROR);
            }
            catch
            {
                return Result<string>.Failure(ResponseStatus.HTTP_RESPONSE_ERROR);
            }
        }

        private string GetContentType(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".webp" => "image/webp",
                _ => "image/jpeg"
            };
        }
    }
}
