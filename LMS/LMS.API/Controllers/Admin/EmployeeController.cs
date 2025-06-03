using AutoMapper;
using LMS.API.DTOs.Admin.Employee;
using LMS.API.Helpers;
using LMS.Application.Abstractions.Services.ImagesServices;
using LMS.Application.DTOs.Admin.Employees;
using LMS.Application.Features.Admin.Employees.Command.CreateDepartment;
using LMS.Application.Features.Admin.Employees.Command.DeleteEmployee;
using LMS.Application.Features.Admin.Employees.Command.TransferEmployee;
using LMS.Application.Features.Admin.Employees.Command.UpdateEmployee;
using LMS.Application.Features.Admin.Employees.Queries.GetAllGetAllEmployees;
using LMS.Application.Features.Admin.Employees.Queries.GetEmployeeById;
using LMS.Common.Enums;
using LMS.Common.Results;
using LMS.Domain.Enums.Commons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IApiImageUploadHelper _uploadHelper;

        public EmployeeController(
            IMapper mapper,
            IMediator mediator,
            IApiImageUploadHelper uploadHelper)
        {
            _mapper = mapper;
            _mediator = mediator;
            _uploadHelper = uploadHelper;
        }



        [HttpGet("get-all-employees")]
        public async Task<ActionResult<ICollection<EmployeeOverviewDto>>> GetAll()
        {
            var response = await _mediator.Send(new GetAllEmployeesQuery());

            return Ok(response);
        }


        [HttpGet("get-employee/{id:guid}")]
        public async Task<ActionResult<EmployeeDetailsDto>> GetEmployeeById(Guid id, [FromQuery] Language language)
        {
            var response = await _mediator.Send(new GetEmployeeByIdQuery(id, language));

            return response is null? NotFound() : Ok(response);
        }




        [HttpPost("add-employee")]
        public async Task<ActionResult<Result<EmployeeCreatignResultDto>>> AddEmployee([FromForm]EmployeeCreateRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var picutreUrl = await _uploadHelper.UploadFormFileAsync(request.ProfilePicture);

            if (picutreUrl.IsFailed || picutreUrl.Value is null)
            {
                return BadRequest(picutreUrl);
            }

            var command = _mapper.Map<CreateEmployeeCommand>(request);

            command = command with { ProfilePictureUrl = picutreUrl.Value };

            var response = await _mediator.Send(command);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }


        
        
        [HttpPost("transfer-employee")]
        public async Task<ActionResult<Result>> TransferEmployee([FromForm]TransferEmployeeRequestDto request)
        {
            var picutreUrl = await _uploadHelper.UploadFormFileAsync(request.AppointmentDesicion);

            if (picutreUrl.IsFailed || picutreUrl.Value is null)
            {
                return BadRequest(picutreUrl);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (request.AppointmentDesicion.Length == 0 || request.AppointmentDesicion is null)
            {
                return BadRequest(Result.Failure(ResponseStatus.FILE_NOT_FOUND));
            }

            var command = _mapper.Map<TransferEmployeeCommand>(request);

            command = command with { AppointmentDecisionUrl = picutreUrl.Value };

            var response = await _mediator.Send(command);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        
        
        [HttpPost("update-employee/{id:guid}")]
        public async Task<ActionResult<Result>> UpdateEmployee(Guid id, [FromBody]EmployeeUpdateRequestDto request)
        {
            var command = _mapper.Map<UpdateEmployeeCommand>(request);

            command = command with { EmployeeId = id };

            var response = await _mediator.Send(command);

            return response.IsSuccess ? NoContent() : NotFound(response);
        }



        [HttpDelete("delete-employee/{id:guid}")]
        public async Task<ActionResult<Result>> DeleteEmployee(Guid id)
        {
            var response = await _mediator.Send(new DeleteEmployeeCommand(id));

            return response.IsSuccess ? Ok(response) : NotFound(response);
        }

    }
}