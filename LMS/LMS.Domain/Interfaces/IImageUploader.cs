namespace LMS.Domain.Interfaces
{
    public interface IImageUploader
    {
        Task<string> UploadImageAsync(Stream imageStream, string fileName);
    }
}
