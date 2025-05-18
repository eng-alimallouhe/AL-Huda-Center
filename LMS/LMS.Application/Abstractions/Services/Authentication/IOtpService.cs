using LMS.Common.Results;
using LMS.Domain.Enums.Users;

namespace LMS.Application.Abstractions.Services.Authentication
{
    public interface IOtpService
    {
        Task<Result<string>> GenerateAndSaveCodeAsync(Guid userId, CodeType codeType);

        Task<Result> VerifyCodeAsync(Guid userId, string code, CodeType codeType);
    }
}