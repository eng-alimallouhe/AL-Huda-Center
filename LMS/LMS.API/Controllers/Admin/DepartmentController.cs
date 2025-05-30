using AutoMapper;
using LMS.API.DTOs.Admin.Department;
using LMS.Application.DTOs.Admin.Departments;
using LMS.Application.Features.Admin.Departments.Command.CreateDepartment;
using LMS.Application.Features.Admin.Departments.Command.DeleteDepartment;
using LMS.Application.Features.Admin.Departments.Command.UpdateDepartment;
using LMS.Application.Features.Admin.Departments.Queries.GetAllDepartments;
using LMS.Application.Features.Admin.Departments.Queries.GetDepartmentById;
using LMS.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DepartmentController(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpGet("get-all-departments/{pageNumber:int}/{pageSize:int}")]
        public async Task<ActionResult<ICollection<DepartmentOverviewDto>>> GetAll(int pageNumber, int pageSize)
        {
            if (pageNumber < 1|| pageSize < 1)
            {
                return BadRequest();
            }
            var response = await _mediator.Send(new GetAllDepartmentsQuery(pageSize, pageNumber));

            return Ok(response);
        }


        [HttpGet("get-department/{id:guid}")]
        public async Task<ActionResult<ICollection<DepartmentDetailsDTO>>> GetDepartmentById(Guid id)
        {
            var response = await _mediator.Send(new GetDepartmentByIdQuery(id));

            return Ok(response);
        }


        [HttpPost("add-department")]
        public async Task<ActionResult<Result>> AddDepartment(DepartmentRequestDto request)
        {
            var command = _mapper.Map<CreateDepartmentCommand>(request);

            var response = await _mediator.Send(command);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }


        [HttpPost("update-department/{id:guid}")]
        public async Task<ActionResult<Result>> UpdateDepartment([FromBody]DepartmentRequestDto request, Guid id)
        {
            var command = _mapper.Map<UpdateDepartmentCommand>(request);

            var response = await _mediator.Send(command);

            return response.IsSuccess ? NoContent() : NotFound(response);
        }


        [HttpDelete("delete-department/{id}")]
        public async Task<ActionResult<Result>> DeleteDepartment(Guid id)
        {
            var response = await _mediator.Send(new DeleteDepartmentCommand(id));

            return response.IsSuccess ? Ok(response) : NotFound();
        }
    }
}
