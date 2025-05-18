using LMS.Application.Abstractions.Services.Authentication;
using LMS.Application.DTOs.AuthenticationDTOs;
using LMS.Common.Enums;
using LMS.Common.Exceptions;
using LMS.Common.Extensions;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LMS.Application.Features.Authentication.Register.Commands.ActivateAccount
{
    public class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand, Result<AuthorizationDTO>>
    {
        private readonly ISoftDeletableRepository<User> _userRepo;
        private readonly ILogger<ActivateAccountCommandHandler> _logger;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public ActivateAccountCommandHandler(
            ISoftDeletableRepository<User> userRepo,
            ILogger<ActivateAccountCommandHandler> logger,
            ITokenGeneratorService tokenGeneratorService)
        {
            _userRepo = userRepo;
            _logger = logger;
            _tokenGeneratorService = tokenGeneratorService;
        }

        public async Task<Result<AuthorizationDTO>> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetBySpecificationAsync(new Specification<User>(
                criteria: user => user.Email.ToNormalize() == request.Email.ToNormalize(),
                includes: [user => user.OtpCode]
                ));

            if (user is null)
            {
                return Result<AuthorizationDTO>.Failure(ResponseStatus.ACCOUNT_NOT_FOUND);
            }

            if (user.IsEmailConfirmed)
            {
                return Result<AuthorizationDTO>.Failure(ResponseStatus.EXISTING_ACCOUNT);
            }

            var otpCode = user.OtpCode;

            if (otpCode is null)
            {
                return Result<AuthorizationDTO>.Failure(ResponseStatus.CODE_NOT_FOUND);
            }

            if (!otpCode.IsUsed || !otpCode.IsVerified)
            {
                return Result<AuthorizationDTO>.Failure(ResponseStatus.ACTIVATION_FAILED);
            }
            user.IsEmailConfirmed = true;
            try
            {
                await _userRepo.UpdateAsync(user);
                var refreshResult = await _tokenGeneratorService.GenerateRefreshTokenAsync(user.UserId);

                if (refreshResult.IsFailed || refreshResult.Value is null)
                {
                    return Result<AuthorizationDTO>.Failure(refreshResult.Status);
                }

                var accessResult = await _tokenGeneratorService.GenerateAccessTokenAsync(user.UserId);
                if (accessResult.IsFailed || accessResult.Value is null)
                {
                    return Result<AuthorizationDTO>.Failure(accessResult.Status);
                }
                return Result<AuthorizationDTO>.Success(new AuthorizationDTO
                {
                    RefreshToken = refreshResult.Value,
                    AccessToken = accessResult.Value,
                }, ResponseStatus.ACTIVATION_SUCCESS);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError($"Error while Activate the account of customer, Customer name: {user.UserName}," +
                    $"\nError Message: {ex.Message}, \n" +
                    $"Error Code: {ex.SqlErrorCode}\n" +
                    $"------------------------------------------------------------------------------------\n");
                return Result<AuthorizationDTO>.Failure(ResponseStatus.ADD_ERROR);
            }
        }
    }
}
