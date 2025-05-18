using LMS.Application.Abstractions.Services.Authentication;
using LMS.Application.DTOs.AuthenticationDTOs;
using LMS.Common.Enums;
using LMS.Common.Extensions;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;

namespace LMS.Infrastructure.Services.Authentication
{
    public class AccountService : IAccountService
    {
        private readonly ISoftDeletableRepository<User> _userRepo;

        public AccountService(ISoftDeletableRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<Result> LogIn(User user, string email, string password)
        {
            if (user.IsDeleted)
            {
                return Result<AuthorizationDTO>.Failure(ResponseStatus.BLOCKED_USER);
            }

            if (user.IsLocked && user.LockedUntil < DateTime.UtcNow)
            {
                return Result.Failure(ResponseStatus.LOCKED_ACCOUNT);
            }

            if (!user.IsEmailConfirmed)
            {
                return Result<AuthorizationDTO>.Failure(ResponseStatus.UNVERIFIED_ACCOUNT);
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.HashedPassword))
            {
                user.FailedLoginAttempts++;

                if (user.FailedLoginAttempts >= 3)
                {
                    user.IsLocked = true;
                    user.LockedUntil = DateTime.UtcNow.AddMinutes(15);
                }

                await _userRepo.UpdateAsync(user);

                return Result.Failure(user.IsLocked ? ResponseStatus.MAX_LOGIN_ATTEMPTS : ResponseStatus.FAILED_ATTEMPT);
            }
            
            user.FailedLoginAttempts = 0;
            user.LastLogIn = DateTime.UtcNow;
            user.IsLocked = false;
            user.LockedUntil = null;

            await _userRepo.UpdateAsync(user);

            if (user.IsTwoFactorEnabled)
            {
                return Result.Failure(ResponseStatus.MORE_STEP_REQUERD);
            }

            return Result.Success(ResponseStatus.VERIFY_SUCCESS);
        }
    }
}
