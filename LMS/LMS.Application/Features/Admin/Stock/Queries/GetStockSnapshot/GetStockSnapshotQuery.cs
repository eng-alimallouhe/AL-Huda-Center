using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Application.DTOs.Common;
using LMS.Domain.Enums.Commons;
using MediatR;

namespace LMS.Application.Features.Admin.Stock.Queries.GetStockSnapshot
{
    public record GetStockSnapshotQuery(
        Language Language,
        int PageNumber,
        int PageSize): IRequest<PagedResult<StockInfromationDto>>;
}
