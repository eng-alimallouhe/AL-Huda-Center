using LMS.Application.DTOs.AuthenticationDTOs;
using LMS.Common.Results;
using MediatR;

namespace LMS.Application.Features.Authentication.Tokens.Command.AuthenticationRefresh
{
    public record AuthenticationRefreshCommand(string RefreshToken, string AccessToken) : IRequest<Result<AuthorizationDTO>>;
}