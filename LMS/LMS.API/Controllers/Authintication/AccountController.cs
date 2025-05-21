using AutoMapper;
using LMS.API.DTOs.Authentication;
using LMS.Application.DTOs.AuthenticationDTOs;
using LMS.Application.Features.Authentication.Accounts.Commands.LogIn;
using LMS.Application.Features.Authentication.Accounts.Commands.TowFactorAuthentication;
using LMS.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers.Authintication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AccountController(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("log-in")]
        public async Task<ActionResult<Result<AuthorizationDTO>>> Login(LoginRequestDTO request)
        {
            var command = _mapper.Map<LogInCommand>(request);

            var response = await _mediator.Send(command);

            if (response.IsFailed)
            {
                return Unauthorized(response);
            }

            return Ok(response);
        }


        [HttpPost("tow-factor-verify/{email}")]
        public async Task<ActionResult<Result<AuthorizationDTO>>> CheckTowFactor(string email)
        {
            var response = await _mediator.Send(new TowFactorAuthenticationCommand(email));

            if (response.IsFailed)
            {
                return Unauthorized(response);
            }

            return Ok(response);
        }
    }
}
