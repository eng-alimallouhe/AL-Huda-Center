using LMS.Application.Abstractions.Services.Authentication;
using LMS.Application.DTOs.AuthenticationDTOs;
using LMS.Common.Enums;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LMS.Application.Features.Authentication.Register.Commands.ActivateAccount
{
    public class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand, Result<AuthorizationDTO>>
    {
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public ActivateAccountCommandHandler(
            IAuthenticationHelper authenticationHelper,
            ISoftDeletableRepository<User> userRepo,
            IBaseRepository<OtpCode> codeRepo,
            ILogger<ActivateAccountCommandHandler> logger,
            ITokenGeneratorService tokenGeneratorService)
        {
            _authenticationHelper = authenticationHelper;
            _tokenGeneratorService = tokenGeneratorService;
        }

        public async Task<Result<AuthorizationDTO>> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
        {
            Result<Guid> authResult = await _authenticationHelper.CanActivateAuth(request.Email, Domain.Enums.Users.CodeType.SignUp);

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
                return Result<AuthorizationDTO>.Failure(accessResult.Status);
            }

            return Result<AuthorizationDTO>.Success(new AuthorizationDTO
            {
                RefreshToken = refreshResult.Value,
                AccessToken = accessResult.Value,
            }, ResponseStatus.ACTIVATION_SUCCESS);
        }
    }
}
