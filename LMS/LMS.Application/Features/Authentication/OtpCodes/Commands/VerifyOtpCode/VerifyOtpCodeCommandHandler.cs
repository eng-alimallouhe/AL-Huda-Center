using LMS.Application.Abstractions.Services.Authentication;
using LMS.Common.Enums;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LMS.Application.Features.Authentication.OtpCodes.Commands.VerifyOtpCode
{
    public class VerifyOtpCodeCommandHandler : IRequestHandler<VerifyOtpCodeCommand, Result>
    {
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly ISoftDeletableRepository<User> _userRepo;
        private readonly ILogger<OtpCode> _logger;


        public VerifyOtpCodeCommandHandler(
            IAuthenticationHelper authenticationHelper,
            ISoftDeletableRepository<User> userRepo,
            ILogger<OtpCode> logger)
        {
            _authenticationHelper = authenticationHelper;
            _userRepo = userRepo;
            _logger = logger;
        }



        public async Task<Result> Handle(VerifyOtpCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByExpressionAsync(user => user.Email.ToLower().Trim() == request.Email.ToLower().Trim());

            if (user is null)
            {
                return Result.Failure(ResponseStatus.ACCOUNT_NOT_FOUND);
            }

            Result verifyResult = await _authenticationHelper.VerifyCodeAsync(user.UserId, request.Code, request.CodeType);

            return verifyResult;
        }
    }
}
