using BCrypt.Net;
using LMS.Application.Abstractions.Services.Authentication;
using LMS.Application.DTOs.AuthenticationDTOs;
using LMS.Common.Enums;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Authentication.Accounts.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<AuthorizationDTO>>
    {
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly ITokenGeneratorService _tokenGeneratorService;
        private readonly ISoftDeletableRepository<User> _userRepo;

        public ResetPasswordCommandHandler(
            IAuthenticationHelper authenticationHelper,
            ITokenGeneratorService tokenGeneratorService,
            ISoftDeletableRepository<User> userRepo)
        {
            _authenticationHelper = authenticationHelper;
            _tokenGeneratorService = tokenGeneratorService;
            _userRepo = userRepo;
        }

        public async Task<Result<AuthorizationDTO>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var authResult = await _authenticationHelper.CanActivateAuth(request.Email, Domain.Enums.Users.CodeType.ResetPassword);

            if (authResult.IsFailed)
            {
                return Result<AuthorizationDTO>.Failure(authResult.Status);
            }

            var userId = authResult.Value;

            var user = await _userRepo.GetByIdAsync(userId);

            if (user is null)
            {
                return Result<AuthorizationDTO>.Failure(ResponseStatus.ACCOUNT_NOT_FOUND);
            }

            user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(user.HashedPassword);

            await _userRepo.UpdateAsync(user);

            var refreshResult = await _tokenGeneratorService.GenerateRefreshTokenAsync(userId);

            if (refreshResult.IsFailed || refreshResult.Value is null)
            {
                return Result<AuthorizationDTO>.Failure(refreshResult.Status);
            }

            var accessResult = await _tokenGeneratorService.GenerateAccessTokenAsync(userId);

            if (accessResult.IsFailed || accessResult.Value is null)
            {
                return Result<AuthorizationDTO>.Failure(refreshResult.Status);
            }

            return Result<AuthorizationDTO>.Success(new AuthorizationDTO
            {
                RefreshToken = refreshResult.Value,
                AccessToken = accessResult.Value
            }, ResponseStatus.AUTHENTICATION_SUCCESS);
        }
    }
}
