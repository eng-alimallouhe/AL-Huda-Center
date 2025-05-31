using LMS.Application.DTOs.Stock;
using LMS.Domain.Enums.Commons;
using MediatR;

namespace LMS.Application.Features.Admin.Reports.Queries.FullStockInventoryReport
{
    public record FullStockInventoryReportQuery(
        Language Language) : IRequest<ICollection<StockSnapshotDto>>;
}
