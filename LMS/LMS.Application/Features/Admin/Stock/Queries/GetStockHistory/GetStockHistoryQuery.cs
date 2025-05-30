using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Application.DTOs.Common;
using LMS.Domain.Enums.Commons;
using MediatR;

namespace LMS.Application.Features.Admin.Stock.Queries.GetStockHistory
{
    public record GetStockHistoryQuery(
        int PageNumber = 0,
        int PageSize = 20, 
        Language Language = Language.EN) : IRequest<PagedResult<InventoryLogDetailsDto>>;
}