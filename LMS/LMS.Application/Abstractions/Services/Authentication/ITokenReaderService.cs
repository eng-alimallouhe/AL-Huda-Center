using System.Security.Claims;

namespace LMS.Application.Abstractions.Services.Authentication
{
    public interface ITokenReaderService
    {
        public Guid? GetUserId(string accessToken);
        public string? GetEmail(string accessToken);
    }
}
