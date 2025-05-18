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

namespace LMS.Application.Features.Authentication.Accounts.LogIn
{
    public class LogInCommandHandler : IRequestHandler<LogInCommand, Result<AuthorizationDTO>>
    {
        private readonly IAccountService _accountService;
        private readonly ISoftDeletableRepository<User> _userRepo;
        private readonly ITokenGeneratorService _tokenGeneratorService;
        private readonly ILogger<LogInCommandHandler> _logger;

        public LogInCommandHandler(
            IAccountService accountService,
            ISoftDeletableRepository<User> userRepo,
            ITokenGeneratorService tokenGeneratorService,
            ILogger<LogInCommandHandler> logger)
        {
            _accountService = accountService;
            _userRepo = userRepo;
            _tokenGeneratorService = tokenGeneratorService;
            _logger = logger;
        }

        public async Task<Result<AuthorizationDTO>> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetBySpecificationAsync(new Specification<User>(
                criteria: user => user.Email.ToNormalize() == request.Email.ToNormalize()
                ));

            if (user is null)
            {
                return Result<AuthorizationDTO>.Failure(ResponseStatus.ACCOUNT_NOT_FOUND);
            }

            Result loginResult;

            try 
            {
                loginResult = await _accountService.LogIn(user, request.Email, request.Password);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError($"Error updating login attempts for {user.UserName}, \n" +
                    $"Error Message: {ex.Message}," +
                    $"Error Code: {ex.SqlErrorCode}," +
                    $"------------------------------------------------------------------------------------\n");
                    
                return Result<AuthorizationDTO>.Failure(ResponseStatus.UPDATE_INFORMATION_ERROR);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating login attempts for {user.UserName}, \n" +
                    $"Error Message: {ex.Message}\n" +
                    $"------------------------------------------------------------------------------------\n");
                    
                return Result<AuthorizationDTO>.Failure(ResponseStatus.UPDATE_INFORMATION_ERROR);
            }

            if (user.IsTwoFactorEnabled)
            {
                return Result<AuthorizationDTO>.Failure(ResponseStatus.MORE_STEP_REQUERD);
            }

            var refreshResult = await _tokenGeneratorService.GenerateRefreshTokenAsync(user.UserId);
            if (refreshResult.IsFailed || refreshResult.Value is null)
                return Result<AuthorizationDTO>.Failure(refreshResult.Status);

            var accessResult = await _tokenGeneratorService.GenerateAccessTokenAsync(user.UserId);
            if (accessResult.IsFailed || accessResult.Value is null)
                return Result<AuthorizationDTO>.Failure(refreshResult.Status);


            return Result<AuthorizationDTO>.Success(new AuthorizationDTO
            {
                AccessToken = accessResult.Value,
                RefreshToken = refreshResult.Value
            }, ResponseStatus.AUTHENTICATION_SUCCESS);
        }
    }
}
