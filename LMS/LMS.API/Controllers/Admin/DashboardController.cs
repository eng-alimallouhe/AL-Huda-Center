using System.ComponentModel.DataAnnotations;
using LMS.API.DTOs.Admin;
using LMS.API.DTOs.QueriesParameter;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Application.Features.Admin.Dashboard.Queries.KPIs.GetBooksCount;
using LMS.Application.Features.Admin.Dashboard.Queries.KPIs.GetNewBooksCount;
using LMS.Application.Features.Admin.Dashboard.Queries.KPIs.GetNewCustomerNumber;
using LMS.Application.Features.Admin.Dashboard.Queries.KPIs.GetPendingOrdersCount;
using LMS.Application.Features.Admin.Dashboard.Queries.KPIs.GetUsersCount;
using LMS.Application.Features.Admin.Dashboard.Queries.LowStockQuantity;
using LMS.Application.Features.Admin.Dashboard.Queries.MonthlyOrders;
using LMS.Application.Features.Admin.Dashboard.Queries.MonthlySales;
using LMS.Application.Features.Admin.Dashboard.Queries.TopFiveSalesProducts;
using LMS.Common.Enums;
using LMS.Domain.Enums.Commons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        
        public DashboardController(
            IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("kpi/{from:datetime}")]
        public async Task<ActionResult<DashboardKpiDto>> GetKpisData(DateTime from)
        {
            var usersCount = await _mediator.Send(new GetUsersCountQuery());
            var newCustomersCount = await _mediator.Send(new GetNewCustomerCountQuery(from));
            var pendingOrdersCount = await _mediator.Send(new GetPendingOrdersCountQuery());
            var booksCount = await _mediator.Send(new GetBooksCountQuery());
            var newBooksCount = await _mediator.Send(new GetNewBooksCountQuery(from));

            return Ok(new DashboardKpiDto()
            {
                UsersCount = usersCount.NumberOfUsers,
                EmployeesCount = usersCount.NumberOfEmployees,
                CustomersCount = usersCount.NumberOfCustomers,
                NewCustomersCount = newCustomersCount,
                PendingOrdersCount = pendingOrdersCount,
                BooksCount = booksCount,
                NewBooksCount = newBooksCount
            });
        }


        [HttpGet("low-stock")]
        public async Task<ActionResult<ICollection<StockInfromationDto>>> GetLowStock([FromQuery][Required] Language language, [FromQuery][Required] int MaxQuantity)
        {
            var command = new LowStockQuantityQuery(MaxQuantity, language);

            var response = await _mediator.Send(command);

            return Ok(response);
        }


        [HttpGet("monthly-orders")]
        public async Task<ActionResult<ICollection<MonthlyOrdersDto>>> GetMonthlyOrders([FromQuery][Required] DateQueryParameter queryParameter)
        {
            if (queryParameter.From >= queryParameter.To)
            {
                return BadRequest(ResponseStatus.UNVALIDE_PARAMETERS);
            }

            var command = new MonthlyOrdersQuery(queryParameter.From, queryParameter.To);

            var response = await _mediator.Send(command);

            return Ok(response);
        }


        [HttpGet("monthly-sales")]
        public async Task<ActionResult<ICollection<MonthlySalesDto>>> GetMonthlySales([FromQuery][Required] DateQueryParameter queryParameter)
        {
            if (queryParameter.From >= queryParameter.To)
            {
                return BadRequest(ResponseStatus.UNVALIDE_PARAMETERS);
            }

            var command = new MonthlySalesQuery(queryParameter.From, queryParameter.To);

            var response = await _mediator.Send(command);

            return Ok(response);
        }

       
        [HttpGet("top-sales-Products")]
        public async Task<ActionResult<ICollection<TopSellingProductDto>>> GetTopSalesProducts([FromQuery][Required] Language language)
        {
            var command = new TopFiveSalesProductsQuery(language);

            var reposne = await _mediator.Send(command);

            return Ok(reposne);
        }
    }
}