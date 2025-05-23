using LMS.Common.Results;

namespace LMS.API.Helpers
{
    public interface IApiImageUploadHelper
    {
        Task<Result<string>> UploadFormFileAsync(IFormFile? file);
    }
}
