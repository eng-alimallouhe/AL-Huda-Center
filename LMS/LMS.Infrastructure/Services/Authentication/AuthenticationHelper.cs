using System.Text.RegularExpressions;
using AutoMapper;
using LMS.Application.Abstractions.Services.Authentication;
using LMS.Application.Abstractions.Services.Helpers;
using LMS.Application.Features.Authentication.Register.Commands.CreateTempAccount;
using LMS.Common.Enums;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Financial.Levels;
using LMS.Domain.Entities.Users;
using LMS.Domain.Enums.Users;

namespace LMS.Infrastructure.Services.Authentication
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private readonly IRandomGeneratorService _randomGenerator;
        private readonly IBaseRepository<OtpCode> _codeRepo;
        private readonly ISoftDeletableRepository<Customer> _customerRepo;
        private readonly ISoftDeletableRepository<User> _userRepo;
        private readonly ISoftDeletableRepository<LoyaltyLevel> _levelRepo;
        private readonly ISoftDeletableRepository<Role> _roleRepo;
        private readonly IMapper _mapper;

        public AuthenticationHelper(
            IRandomGeneratorService randomGenerator,
            IBaseRepository<OtpCode> codeRepo,
            ISoftDeletableRepository<Customer> customerRepo,
            ISoftDeletableRepository<User> userRepo,
            ISoftDeletableRepository<LoyaltyLevel> levelRepo,
            ISoftDeletableRepository<Role> roleRepo,
            IMapper mapper)
        {
            _randomGenerator = randomGenerator;
            _codeRepo = codeRepo;
            _customerRepo = customerRepo;
            _userRepo = userRepo;
            _levelRepo = levelRepo;
            _roleRepo = roleRepo;
            _mapper = mapper;
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

            await _codeRepo.UpdateAsync(otp);

            return Result.Success(ResponseStatus.TASK_COMPLETED);
        }



        public async Task<Result> CreateAndSaveAccountAsync(CreateTempAccountCommand request)
        {
            if (!IsStrongPassword(request.Password))
            {
                return Result.Failure(ResponseStatus.WEAK_PASSWORD);
            }

            var oldUser = await _userRepo.GetByExpressionAsync(user => user.Email.ToLower().Trim() == request.Email.ToLower().Trim());

            if (oldUser is not null)
            {
                return Result.Failure(ResponseStatus.EXISTING_ACCOUNT);
            }

            var newCustomer = _mapper.Map<Customer>(request);


            newCustomer.HashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var customersRole = await _roleRepo.GetByExpressionAsync(
                role => role.RoleType.ToLower() == "customer"
                );

            if (customersRole is null)
            {
                return Result.Failure(ResponseStatus.BACK_ERROR);
            }

            var firstLevel = await _levelRepo.GetByExpressionAsync(
                level => level.Translations.Any(t => t.LevelName.ToLower() == "bronze")
                );


            if (firstLevel is null)
            {
                return Result.Failure(ResponseStatus.BACK_ERROR);
            }

            newCustomer.Level = firstLevel;
            newCustomer.Role = customersRole;
            newCustomer.LevelId = firstLevel.LevelId;
            newCustomer.RoleId = customersRole.RoleId;

            await _customerRepo.AddAsync(newCustomer);

            return Result.Success(ResponseStatus.TASK_COMPLETED);
        }



        public async Task<Result<Guid>> CanActivateAuth(string email, CodeType activationType)
        {
            var user = await _userRepo.GetBySpecificationAsync(new Specification<User>(
                criteria: user => user.Email.ToLower().Trim() == email.ToLower().Trim(),
                includes: ["OtpCode"]
                ));

            if (user is null)
            {
                return Result<Guid>.Failure(ResponseStatus.ACCOUNT_NOT_FOUND);
            }

            var otp = user.OtpCode;

            if (otp is null)
            {
                return Result<Guid>.Failure(ResponseStatus.ACTIVATION_FAILED);
            }

            if (otp.CodeType != activationType)
            {
                return Result<Guid>.Failure(ResponseStatus.CODE_NOT_FOUND);
            }

            if (otp.IsUsed || !otp.IsVerified)
            {
                return Result<Guid>.Failure(ResponseStatus.CODE_NOT_FOUND);
            }

            if (otp.CodeType == CodeType.SignUp)
            {
                user.IsEmailConfirmed = true;
            }

            otp.IsUsed = true;
            user.LastLogIn = DateTime.UtcNow;

            await _userRepo.UpdateAsync(user);
            await _codeRepo.UpdateAsync(otp);

            return Result<Guid>.Success(user.UserId, ResponseStatus.ACTIVATION_SUCCESS);
        }


        public async Task<Result> LogIn(User user, string password)
        {
            
            if (user.IsDeleted)
            {
                return Result.Failure(ResponseStatus.BLOCKED_USER);
            }

            if (user.IsLocked && user.LockedUntil < DateTime.UtcNow)
            {
                return Result.Failure(ResponseStatus.LOCKED_ACCOUNT);
            }

            if (!user.IsEmailConfirmed)
            {
                return Result.Failure(ResponseStatus.UNVERIFIED_ACCOUNT);
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


        //this function check if the provided password is contain atleast 1 lower-case and 1 upper-case and 1 number
        private bool IsStrongPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (password.Length < 8)
                return false;

            if (!Regex.IsMatch(password, "[A-Z]"))
                return false;

            if (!Regex.IsMatch(password, "[a-z]"))
                return false;

            if (!Regex.IsMatch(password, "[0-9]"))
                return false;

            if (!Regex.IsMatch(password, "[^a-zA-Z0-9]"))
                return false;

            return true;
        }
    }
}