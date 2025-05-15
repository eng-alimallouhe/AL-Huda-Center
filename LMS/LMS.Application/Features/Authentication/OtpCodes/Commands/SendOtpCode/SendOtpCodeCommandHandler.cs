using LMS.Common.Enums;
using LMS.Common.Exceptions;
using LMS.Common.Interfaces;
using LMS.Common.Results;
using LMS.Common.Specifications;
using LMS.Domain.Entities.Users;
using LMS.Domain.Enums.Users;
using LMS.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LMS.Application.Features.Authentication.OtpCodes.Commands.SendOtpCode
{
    public class SendOtpCodeCommandHandler : IRequestHandler<SendOtpCodeCommand, Result>
    {
        private readonly IEmailSenderService _emailSenderService;
        private readonly ISoftDeletableRepository<User> _userRepo;
        private readonly IBaseRepository<OtpCode> _codeRepo;
        private readonly ILogger<SendOtpCodeCommandHandler> _logger;
        private readonly IEmailTemplateReaderService _emailTemplateReaderService;
        private readonly IRandomGeneratorService _randomGeneratorService;

        public SendOtpCodeCommandHandler(
            IEmailSenderService emailSenderService,
            ISoftDeletableRepository<User> userRepo,
            IBaseRepository<OtpCode> codeRepo,
            IEmailTemplateReaderService emailTemplateReaderService,
            IRandomGeneratorService randomGeneratorService,
            ILogger<SendOtpCodeCommandHandler> logger)
        {
            _emailSenderService = emailSenderService;
            _userRepo = userRepo;
            _codeRepo = codeRepo;
            _logger = logger;
            _emailTemplateReaderService = emailTemplateReaderService;
            _randomGeneratorService = randomGeneratorService;
        }

        public async Task<Result> Handle(SendOtpCodeCommand request, CancellationToken cancellationToken)
        {
            var template = _emailTemplateReaderService.ReadTemplate(request.Language, request.Purpose);

            if (template is null)
            {
                return Result.Failure(ResponseStatus.FILE_NOT_FOUND);
            }

            var user = await _userRepo.GetBySpecificationAsync(new Specification<User>(
                criteria: user => user.Email.ToLowerInvariant().Trim() == request.Email.ToLowerInvariant().Trim(),
                includes: [user => user.OtpCode]
                ));

            if (user is null)
            {
                return Result.Failure(ResponseStatus.ACCOUNT_NOT_FOUND);
            }

            var otpCode = user.OtpCode;

            if (otpCode is not null)
            {
                if (otpCode.IsUsed)
                {
                    return Result.Failure(ResponseStatus.CODE_IS_EXPIRED);
                }

                if (otpCode.CreatedAt < DateTime.UtcNow.AddMinutes(-15))
                {
                    return Result.Failure(ResponseStatus.INDEFINITE_TIME_PERIOD);
                }

                try
                {
                    await _codeRepo.HardDeleteAsync(otpCode.OtpCodeId);
                }
                catch (DatabaseException ex)
                {
                    _logger.LogError($"Error while deleting the code for user : {user.UserName}, \n" +
                        $"Error message: {ex.Message}");
                    return Result.Failure(ResponseStatus.DELETE_ERROR);
                }
                catch (EntityNotFoundException)
                {
                    return Result.Failure(ResponseStatus.CODE_NOT_FOUND);
                }
            }

            string code = _randomGeneratorService.GenerateSexDigitsCode();
            string body = template.Replace("{{name}}", user.UserName)
                                    .Replace("{{code}}", code);

            var codeIndex = (int) request.Purpose;

            var newOtpCode = new OtpCode
            {
                HashedValue = BCrypt.Net.BCrypt.HashPassword(code),
                UserId = user.UserId,
                CodeType = (CodeType)codeIndex,
            };

            try
            {
                await _codeRepo.AddAsync(newOtpCode);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError($"Error whiel adding the code for the user: {user.UserName}, \n" +
                    $"Error Message: {ex.Message}, \n" +
                    $"Error Code: {ex.SqlErrorCode} \n" +
                    $"------------------------------------------------------------------------------------\n");

                return Result.Failure(ResponseStatus.BACK_ERROR);
            }

            int currentAttempt = 0;
            bool isSended = false;

            while (currentAttempt <= 3 && !isSended)
            {
                try
                {
                    await _emailSenderService.SendEmailAsync(request.Email, $"Verify {newOtpCode.CodeType.ToString()}", body);
                    isSended = true;
                }
                catch (Exception ex)
                {
                    if (currentAttempt >= 3)
                    {
                        _logger.LogError($"Error while sending otp code for the user: {user.UserName}, \n" +
                            $"Error Message: {ex.Message}, \n" +
                            $"------------------------------------------------------------------------------------\n");
                        return Result.Failure(ResponseStatus.CODE_ERROR);
                    }
                    currentAttempt++;                
                    await Task.Delay(2000);
                }
            }
            return Result.Success(ResponseStatus.SUCCESSS_CODE_SEND);
        }
    }
}
