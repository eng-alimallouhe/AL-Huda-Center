using LMS.API.DTOs.QueriesParameter;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Application.DTOs.Admin.Financial;
using LMS.Application.DTOs.Admin.Sales;
using LMS.Application.DTOs.Common;
using LMS.Application.Features.Admin.Sales.Queries.ExpensesReport;
using LMS.Application.Features.Admin.Sales.Queries.GetAllPayments;
using LMS.Application.Features.Admin.Sales.Queries.Revenues;
using LMS.Application.Features.Admin.Sales.Queries.RevenuesReposrt;
using LMS.Application.Features.Admin.Sales.Queries.TopSalesProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("expenses")]
        public async Task<ActionResult<PagedResult<PaymentDetailsDto>>> GetAllExpenses([FromQuery] PagedQueryParameters queryParameters)
        {
            var command = new GetAllPaymentsQuery(queryParameters.PageNumber, queryParameters.PageSize);

            var response = await _mediator.Send(command);

            return Ok(response);
        }


        [HttpGet("revenues")]
        public async Task<ActionResult<PagedResult<FinancialDetailsDto>>> GetAllRevenues([FromQuery] PagedQueryParameters queryParameters)
        {
            var command = new RevenuesQuery(queryParameters.PageNumber, queryParameters.PageSize);

            var response = await _mediator.Send(command);

            return Ok(response);
        }

        
        [HttpGet("products/top-salling")]
        public async Task<ActionResult<ICollection<TopSellingProductDto>>> GetTopSalesProducts([FromQuery] CountQueryParameters queryParameters)
        {
            var command = new TopSalesProductsQuery(queryParameters.TopCount, queryParameters.Language);

            var response = await _mediator.Send(command);

            return Ok(response);
        }


        [HttpGet("revenues/by-period")]
        public async Task<ActionResult<ICollection<FinancialDetailsDto>>> GetRevenuesByPeriod([FromQuery] DateQueryParameter queryParameter)
        {
            var command = new RevenuesReposrtQuery(queryParameter.From, queryParameter.To);

            var response = await _mediator.Send(command);

            return Ok(response);
        }



        [HttpGet("expenses/by-period")]
        public async Task<ActionResult<ICollection<PaymentDetailsDto>>> GetExpensesByPeriod([FromQuery] DateQueryParameter queryParameter)
        {
            var command = new ExpensesReportQuery(queryParameter.From, queryParameter.To);

            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
