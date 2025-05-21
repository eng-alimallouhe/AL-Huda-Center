using AutoMapper;
using LMS.API.DTOs.Authentication;
using LMS.Application.DTOs.AuthenticationDTOs;
using LMS.Application.Features.Authentication.Register.Commands.ActivateAccount;
using LMS.Application.Features.Authentication.Register.Commands.CreateTempAccount;
using LMS.Application.Features.Authentication.Register.Queries.CheckUsernameAvailability;
using LMS.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers.Authintication
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RegisterController(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet("check-username-availability/{username}", Name = "CheckUsernameAvailability")]
        public async Task<ActionResult<Result>> CheckUsernameAvailability(string username)
        {
            var response = await _mediator.Send(new CheckUsernameAvailabilityQuery(username));

            if (response.IsFailed)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("create-temp-account")]
        public async Task<ActionResult<Result>> CreateTempAccount(RegisterRequestDTO request)
        {
            var command = _mapper.Map<CreateTempAccountCommand>(request);

            var response = await _mediator.Send(command);

            if (response.IsFailed)
            {
                return BadRequest(response);
            }

            return Created();
        }

        [HttpPost("activate-account/{email}")]
        public async Task<ActionResult<AuthorizationDTO>> ActivateAccount(string email)
        {
            var command = new ActivateAccountCommand(email);

            var response = await _mediator.Send(command);

            if (response.IsFailed)
            {
                return Unauthorized(response);
            }

            return Ok(response);
        }
    }
}
