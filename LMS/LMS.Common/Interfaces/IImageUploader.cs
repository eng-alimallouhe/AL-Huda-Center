using LMS.Common.Results;

namespace LMS.Common.Interfaces
{
    public interface IImageUploader
    {
        Task<Result<string>> UploadImageAsync(Stream imageStream, string fileName);
    }
}
