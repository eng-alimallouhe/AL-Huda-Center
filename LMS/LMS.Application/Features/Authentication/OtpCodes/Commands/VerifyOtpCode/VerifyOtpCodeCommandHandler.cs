using LMS.Common.Enums;
using LMS.Common.Exceptions;
using LMS.Common.Results;
using LMS.Domain.Entities.Users;
using LMS.Domain.Interfaces;
using LMS.Infrastructure.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LMS.Application.Features.Authentication.OtpCodes.Commands.VerifyOtpCode
{
    public class VerifyOtpCodeCommandHandler : IRequestHandler<VerifyOtpCodeCommand, Result>
    {
        private readonly ISoftDeletableRepository<User> _userRepo;
        private readonly IBaseRepository<OtpCode> _codeRepo;
        private readonly ILogger<OtpCode> _logger;

        public VerifyOtpCodeCommandHandler(
            ISoftDeletableRepository<User> userRepo,
            IBaseRepository<OtpCode> codeRepo,
            ILogger<OtpCode> logger)
        {
            _userRepo = userRepo;
            _codeRepo = codeRepo;
            _logger = logger;
        }

        public async Task<Result> Handle(VerifyOtpCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetBySpecificationAsync(new Specification<User>(
                criteria: user => user.Email.ToLowerInvariant().Trim() == request.Email.ToLowerInvariant().Trim(),
                includes: [user => user.OtpCode]
                ));

            if (user is null)
            {
                return Result.Failure(ResponseStatus.ACCOUNT_NOT_FOUND);
            }

            var existingCode = user.OtpCode;

            bool isCodeNotExists = existingCode is null;
            bool isUsed = existingCode?.IsUsed is true;
            bool isExpired = existingCode?.ExpiredAt < DateTime.UtcNow;
            bool isBlocked = existingCode?.FailedAttempts >= 3;
            bool isMatch = false;

            if (isCodeNotExists)
                return Result.Failure(ResponseStatus.CODE_NOT_FOUND);
            
            isMatch = BCrypt.Net.BCrypt.Verify(request.Code, existingCode!.HashedValue);

            if (isUsed)
                return Result.Failure(ResponseStatus.CODE_IS_EXPIRED);
            if (isExpired)
                return Result.Failure(ResponseStatus.CODE_IS_EXPIRED);

            if (isBlocked)
                return Result.Failure(ResponseStatus.HIT_MAX_ATTEMPTS);

            if (!isMatch)
            {
                existingCode!.FailedAttempts += 1;

                try
                {
                    await _codeRepo.UpdateAsync(existingCode);
                }
                catch (DatabaseException ex)
                {
                    _logger.LogError($"Error while updating OTP Code for the user: {user.UserName}, \n" +
                        $"Error Message: {ex.Message},\n" +
                        $"Error Code: {ex.SqlErrorCode}\n " +
                        $"------------------------------------------------------------------------------------\n");
                    
                    return Result.Failure(ResponseStatus.UPDATE_INFORMATION_ERROR);
                }

                return Result.Failure(ResponseStatus.FAILED_ATTEMPT);
            }
            try
            {
                existingCode!.IsUsed = true;
                await _codeRepo.UpdateAsync(existingCode!);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError($"Error while updating OTP Code for the user: {user.UserName}, \n" +
                        $"Error Message: {ex.Message},\n" +
                        $"Error Code: {ex.SqlErrorCode}\n " +
                        $"------------------------------------------------------------------------------------\n");

                return Result.Failure(ResponseStatus.UPDATE_INFORMATION_ERROR);
            }

            return Result.Success(ResponseStatus.VERIFY_SUCCESS);
        }
    }
}
