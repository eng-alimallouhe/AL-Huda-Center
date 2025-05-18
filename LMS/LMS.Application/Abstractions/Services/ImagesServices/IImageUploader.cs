using LMS.Common.Results;

namespace LMS.Application.Abstractions.Services.ImagesServices
{
    public interface IImageUploader
    {
        Task<Result<string>> UploadImageAsync(Stream imageStream, string fileName);
    }
}