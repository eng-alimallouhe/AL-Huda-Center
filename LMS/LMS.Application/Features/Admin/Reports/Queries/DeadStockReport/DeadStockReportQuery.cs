using LMS.Application.DTOs.Stock;
using LMS.Domain.Enums.Commons;
using MediatR;

namespace LMS.Application.Features.Admin.Reports.Queries.DeadStockReport
{
    public record DeadStockReportQuery(
        DateTime From,
        Language Language) : IRequest<ICollection<DeadStockDto>>;
}
