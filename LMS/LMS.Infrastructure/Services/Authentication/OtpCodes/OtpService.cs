using LMS.Application.Abstractions.Services.Authentication;
using LMS.Application.Abstractions.Services.Helpers;
using LMS.Common.Enums;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using LMS.Domain.Enums.Users;

namespace LMS.Infrastructure.Services.Authentication.OtpCodes
{
    public class OtpService : IOtpService
    {
        private readonly IRandomGeneratorService _randomGenerator;
        private readonly IBaseRepository<OtpCode> _codeRepo;

        public OtpService(
            IRandomGeneratorService randomGenerator,
            IBaseRepository<OtpCode> codeRepo)
        {
            _randomGenerator = randomGenerator;
            _codeRepo = codeRepo;
        }

        public async Task<Result<string>> GenerateAndSaveCodeAsync(Guid userId, CodeType codeType)
        {
            var oldCode = await _codeRepo.GetByExpressionAsync(c => c.UserId == userId);

            if (oldCode is not null)
            {
                if (oldCode.CreatedAt > DateTime.UtcNow.AddMinutes(-15))
                {
                    return Result<string>.Failure(ResponseStatus.INDEFINITE_TIME_PERIOD);
                }
                await _codeRepo.HardDeleteAsync(oldCode.OtpCodeId);
            }

            var code = _randomGenerator.GenerateSexDigitsCode();

            var newCode = new OtpCode
            {
                HashedValue = BCrypt.Net.BCrypt.HashPassword(code),
                UserId = userId,
                CodeType = codeType
            };

            await _codeRepo.AddAsync(newCode);

            return Result<string>.Success(code.ToString(), ResponseStatus.SUCCESSS_CODE_SEND);
        }


        public async Task<Result> VerifyCodeAsync(Guid userId, string code, CodeType codeType)
        {
            var otp = await _codeRepo.GetByExpressionAsync(c => c.UserId == userId);

            if (otp is null)
            {
                return Result.Failure(ResponseStatus.CODE_NOT_FOUND);
            }

            if (otp.CodeType != codeType)
            {
                return Result.Failure(ResponseStatus.WRONGE_CODE_TYPE);
            }

            if (otp.ExpiredAt < DateTime.UtcNow)
            {
                return Result.Failure(ResponseStatus.CODE_IS_EXPIRED);
            }

            if (otp.IsUsed || otp.IsVerified)
            {
                return Result.Failure(ResponseStatus.CODE_IS_EXPIRED);
            }

            var isMathch = BCrypt.Net.BCrypt.Verify(code, otp.HashedValue);

            if (!isMathch)
            {
                otp.FailedAttempts += 1;
                
                if (otp.FailedAttempts >= 3)
                {
                    otp.IsUsed = true;
                    await _codeRepo.UpdateAsync(otp);
                    return Result.Failure(ResponseStatus.HIT_MAX_ATTEMPTS);
                }

                await _codeRepo.UpdateAsync(otp);
                return Result.Failure(ResponseStatus.FAILED_ATTEMPT);
            }

            otp.IsVerified = true;
            otp.IsUsed = true;

            await _codeRepo.UpdateAsync(otp);

            return Result.Success(ResponseStatus.TASK_COMPLETED);
        }
    }
}
