using LMS.API.DTOs.QueriesParameter;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Application.DTOs.Common;
using LMS.Application.DTOs.Stock;
using LMS.Application.Features.Admin.Stock.Queries.GetDeadStock;
using LMS.Application.Features.Admin.Stock.Queries.GetStockHistory;
using LMS.Application.Features.Admin.Stock.Queries.GetStockSnapshot;
using LMS.Domain.Enums.Commons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        
        
        [HttpGet("dead-stock")]
        public async Task<ActionResult<ICollection<DeadStockDto>>> GetDeadStock([FromQuery] Language language, [FromQuery] PagedQueryParameters queryParameter, [FromQuery] DateTime from)
        {
            var command = new GetDeadStockQuery(language, queryParameter.PageNumber, queryParameter.PageSize, from);

            var response = await _mediator.Send(command);

            return Ok(response);
        }


        
        
        [HttpGet("stock-history")]
        public async Task<ActionResult<PagedResult<InventoryLogDetailsDto>>> GetHistoryStock([FromQuery] Language language, [FromQuery] PagedQueryParameters queryParameter, [FromQuery] DateTime from)
        {
            var command = new GetStockHistoryQuery(language, queryParameter.PageNumber, queryParameter.PageSize);

            var response = await _mediator.Send(command);

            return Ok(response);
        }



        [HttpGet("stock-snapshot")]
        public async Task<ActionResult<PagedResult<StockInfromationDto>>> GetStockSnapshot([FromQuery] Language language, [FromQuery] PagedQueryParameters queryParameter, [FromQuery] DateTime from)
        {
            var command = new GetStockSnapshotQuery(language, queryParameter.PageNumber, queryParameter.PageSize);

            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
