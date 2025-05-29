using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Domain.Enums.Commons;
using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.LowStockQuantity
{
    public record LowStockQuantityQuery(
        int MaxQuantity,
        Language Language) : IRequest<ICollection<StockInfromationDto>>;
}