using LMS.Application.Abstractions.Services.Authentication;
using LMS.Application.Abstractions.Services.EmailSender;
using LMS.Application.Abstractions.Services.EmailServices;
using LMS.Common.Enums;
using LMS.Common.Exceptions;
using LMS.Common.Results;
using LMS.Domain.Abstractions;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LMS.Application.Features.Authentication.OtpCodes.Commands.SendOtpCode
{
    public class SendOtpCodeCommandHandler : IRequestHandler<SendOtpCodeCommand, Result>
    {
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSenderService _emailSenderService;
        private readonly ISoftDeletableRepository<User> _userRepo;
        private readonly ILogger<SendOtpCodeCommandHandler> _logger;
        private readonly IEmailTemplateReaderService _emailTemplateReaderService;


        public SendOtpCodeCommandHandler(
            IAuthenticationHelper authenticationHelper,
            IUnitOfWork unitOfWork,
            IEmailSenderService emailSenderService,
            ISoftDeletableRepository<User> userRepo,
            IBaseRepository<OtpCode> codeRepo,
            IEmailTemplateReaderService emailTemplateReaderService,
            ILogger<SendOtpCodeCommandHandler> logger)
        {
            _authenticationHelper = authenticationHelper;
            _unitOfWork = unitOfWork;
            _emailSenderService = emailSenderService;
            _userRepo = userRepo;
            _logger = logger;
            _emailTemplateReaderService = emailTemplateReaderService;
        }

        public async Task<Result> Handle(SendOtpCodeCommand request, CancellationToken cancellationToken)
        {

            var purpose = (EmailPurpose)(int) request.CodeType;

            var user = await _userRepo.GetByExpressionAsync(user => user.Email.ToLower().Trim() == request.Email.ToLower().Trim());

            if (user is null)
            {
                return Result.Failure(ResponseStatus.ACCOUNT_NOT_FOUND);
            }

            var template = _emailTemplateReaderService.ReadTemplate(user.Language, purpose);

            if (template is null)
            {
                return Result.Failure(ResponseStatus.FILE_NOT_FOUND);
            }

            await _unitOfWork.BeginTransactionAsync();

            Result<string> codeResult; 

            try
            {
                codeResult = await _authenticationHelper.GenerateAndSaveCodeAsync(user.UserId, request.CodeType);
            }

            catch(DatabaseException ex)
            {
                _logger.LogError($"Error while deleting the code for user : {user.UserName}, \n" +
                        $"Error message: {ex.Message}, \n" +
                        $"Error Code : {ex.SqlErrorCode}\n" +
                        $"------------------------------------------------------------------------------------\n");
                await _unitOfWork.RollbackTransactionAsync();
                return Result.Failure(ResponseStatus.CODE_ERROR);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting the code for user : {user.UserName}, \n" +
                    $"Error message: {ex.Message}, \n" +
                    $"------------------------------------------------------------------------------------\n");
                await _unitOfWork.RollbackTransactionAsync();
                return Result.Failure(ResponseStatus.CODE_ERROR);
            }

            if (codeResult.IsFailed || codeResult.Value is null)
            {
                return Result.Failure(codeResult.Status);
            }

            string body = template.Replace("{{name}}", user.UserName)
                                    .Replace("{{code}}", codeResult.Value);

            int currentAttempt = 0;
            bool isSended = false;

            while (currentAttempt <= 3 && !isSended)
            {
                try
                {
                    await _emailSenderService.SendEmailAsync(request.Email, $"Verify {request.CodeType}", body);
                    isSended = true;
                }
                catch (Exception ex)
                {
                    if (currentAttempt >= 3)
                    {
                        _logger.LogError($"Error while sending otp code for the user: {user.UserName}, \n" +
                            $"Error Message: {ex.Message}, \n" +
                            $"------------------------------------------------------------------------------------\n");
                        await _unitOfWork.RollbackTransactionAsync();
                        return Result.Failure(ResponseStatus.CODE_ERROR);
                    }
                    currentAttempt++;                
                    await Task.Delay(1000).ConfigureAwait(false);
                }
            }
            
            await _unitOfWork.CommitTransactionAsync();

            return Result.Success(ResponseStatus.SUCCESSS_CODE_SEND);
        }
    }
}
