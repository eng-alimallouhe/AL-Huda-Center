using LMS.Application.Abstractions.Services.Authentication;
using LMS.Application.DTOs.AuthenticationDTOs;
using LMS.Common.Results;
using MediatR;

namespace LMS.Application.Features.Authentication.Tokens.Command.AuthenticationRefresh
{
    public class AuthenticationRefreshCommandHandler : IRequestHandler<AuthenticationRefreshCommand, Result<AuthorizationDTO>>
    {
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public AuthenticationRefreshCommandHandler(
            ITokenGeneratorService tokenGeneratorService)
        {
            _tokenGeneratorService = tokenGeneratorService;
        }

        public async Task<Result<AuthorizationDTO>> Handle(AuthenticationRefreshCommand request, CancellationToken cancellationToken)
        {
            return await _tokenGeneratorService.ValidateTokenAsync(request.RefreshToken, request.AccessToken);
        }
    }
}
