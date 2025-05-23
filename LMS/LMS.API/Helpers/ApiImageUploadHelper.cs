using LMS.Application.Abstractions.Services.ImagesServices;
using LMS.Common.Enums;
using LMS.Common.Results;

namespace LMS.API.Helpers
{
    public class ApiImageUploadHelper : IApiImageUploadHelper
    {
        private readonly IImageUploader _imageUploader;
        private readonly IConfiguration _configuration;

        public ApiImageUploadHelper(
            IImageUploader imageUploader,
            IConfiguration configuration)
        {
            _imageUploader = imageUploader;
            _configuration = configuration;
        }

        public async Task<Result<string>> UploadFormFileAsync(IFormFile? file)
        {
            if (file is null || file.Length == 0)
            {
                var url = _configuration["ExternalResource:defualtProfilePicture"] ?? "";
                return Result<string>.Success(url, ResponseStatus.TASK_COMPLETED);
            }

            await using var stream = file.OpenReadStream();

            var response = await _imageUploader.UploadImageAsync(stream, file.FileName);

            if (response.IsFailed)
            {
                var url = _configuration["ExternalResource:defualtProfilePicture"] ?? "";
                return Result<string>.Success(url, ResponseStatus.TASK_COMPLETED);
            }

            return response;
        }
    }
}
