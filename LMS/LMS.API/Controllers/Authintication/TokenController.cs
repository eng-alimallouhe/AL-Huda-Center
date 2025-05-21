using AutoMapper;
using LMS.API.DTOs.Authentication;
using LMS.Application.DTOs.AuthenticationDTOs;
using LMS.Application.Features.Authentication.Tokens.Command.AuthenticationRefresh;
using LMS.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers.Authintication
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public TokenController(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpPost("auth-refresh")]
        public async Task<ActionResult<Result<AuthorizationDTO>>> ValidateAuth(AuthorizationRequestDTO request)
        {
            var command = _mapper.Map<AuthenticationRefreshCommand>(request);

            var response = await _mediator.Send(command);

            if (response.IsFailed)
            {
                return Unauthorized(response);
            }
            return Ok(response);
        }
    }
}
