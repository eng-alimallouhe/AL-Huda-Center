using LMS.Application.Abstractions.Services.Authentication;
using LMS.Application.DTOs.AuthenticationDTOs;
using LMS.Common.Enums;
using LMS.Common.Results;
using MediatR;

namespace LMS.Application.Features.Authentication.Accounts.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<AuthorizationDTO>>
    {
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public ResetPasswordCommandHandler(
            IAuthenticationHelper authenticationHelper,
            ITokenGeneratorService tokenGeneratorService)
        {
            _authenticationHelper = authenticationHelper;
            _tokenGeneratorService = tokenGeneratorService;
        }

        public async Task<Result<AuthorizationDTO>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var authResult = await _authenticationHelper.CanActivateAuth(request.email, Domain.Enums.Users.CodeType.ResetPassword);

            if (authResult.IsFailed)
            {
                return Result<AuthorizationDTO>.Failure(authResult.Status);
            }

            var userId = authResult.Value;

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
