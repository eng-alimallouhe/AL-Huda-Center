using System.ComponentModel.DataAnnotations;
using LMS.API.DTOs.QueriesParameter;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Application.DTOs.Common;
using LMS.Application.DTOs.Users;
using LMS.Application.Features.Admin.Customers.Queries.GetAllCustomers;
using LMS.Application.Features.Admin.Customers.Queries.GetCustomerById;
using LMS.Application.Features.Admin.Customers.Queries.GetInActiveCustomers;
using LMS.Application.Features.Admin.Customers.Queries.GetNewCustomers;
using LMS.Application.Features.Admin.Customers.Queries.TopPayingCustomers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all-customers")]
        public async Task<ActionResult<PagedResult<CustomersOverViewDto>>> GetAllCustomers([FromQuery][Required] PagedQueryParameters queryParameters)
        {
            var command = new GetAllCustomersQuery(queryParameters.PageNumber, queryParameters.PageSize);
        
            var response = await _mediator.Send(command);

            return Ok(response);
        }


        
        [HttpGet("get-customer/{id:guid}")]
        public async Task<ActionResult<CustomerDetailsDto>> GetCustomerById(Guid id)
        {
            var command = new GetCustomerByIdQuery(id);

            var response = await _mediator.Send(command);

            return response is null? NotFound() : Ok(response);
        }


        
        [HttpGet("get-inactive-customers")]
        public async Task<ActionResult<PagedResult<CustomersOverViewDto>>> GetInactiveCustomers([FromQuery] PagedQueryParameters queryParameters, DateTime from)
        {
            var command = new GetInActiveCustomersQuery(from, queryParameters.PageNumber, queryParameters.PageSize);

            var response =  await _mediator.Send(command);

            return Ok(response);
        }


        
        [HttpGet("get-new-customers")]
        public async Task<ActionResult<ICollection<CustomersOverViewDto>>> GetNewCustomers([FromQuery] DateTime startDate)
        {
            var command = new GetNewCustomersQuery(startDate);

            var response = await _mediator.Send(command);

            return Ok(response);
        }

        
        [HttpGet("get-top-paying-customers")]
        public async Task<ActionResult<ICollection<TopPayingCustomerDto>>> GetTopPayingCustomers(int topCount)
        {
            var command = new TopPayingCustomersQuery(topCount);

            var reponse = await _mediator.Send(command);

            return Ok(reponse);
        }
    }
}
