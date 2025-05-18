using LMS.Application.Abstractions.Services.Authentication;
using LMS.Common.Enums;
using LMS.Common.Exceptions;
using LMS.Common.Extensions;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LMS.Application.Features.Authentication.OtpCodes.Commands.VerifyOtpCode
{
    public class VerifyOtpCodeCommandHandler : IRequestHandler<VerifyOtpCodeCommand, Result>
    {
        private readonly IOtpService _otpService;
        private readonly ISoftDeletableRepository<User> _userRepo;
        private readonly ILogger<OtpCode> _logger;


        public VerifyOtpCodeCommandHandler(
            IOtpService otpService,
            ISoftDeletableRepository<User> userRepo,
            ILogger<OtpCode> logger)
        {
            _otpService = otpService;
            _userRepo = userRepo;
            _logger = logger;
        }



        public async Task<Result> Handle(VerifyOtpCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByExpressionAsync(user => user.Email.ToNormalize() == request.Email.ToNormalize());

            if (user is null)
            {
                return Result.Failure(ResponseStatus.ACCOUNT_NOT_FOUND);
            }

            Result verifyResult;

            try
            {
                verifyResult = await _otpService.VerifyCodeAsync(user.UserId, request.Code, request.CodeType);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError($"Error while deleting the code for user : {user.UserName}, \n" +
                    $"Error message: {ex.Message}, \n" +
                    $"Error Code : {ex.SqlErrorCode}\n" +
                    $"------------------------------------------------------------------------------------\n");
                return Result.Failure(ResponseStatus.CODE_ERROR);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting the code for user : {user.UserName}, \n" +
                    $"Error message: {ex.Message}, \n" +
                    $"------------------------------------------------------------------------------------\n");

                return Result.Failure(ResponseStatus.CODE_ERROR);
            }

            return verifyResult;
        }
    }
}
