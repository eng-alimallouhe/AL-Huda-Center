using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Domain.Enums.Commons;
using MediatR;

namespace LMS.Application.Features.Admin.Stock.Queries.InvernotyLogs
{
    public record InventoryLogsQuery(
        int Skip = 0,
        int Take = 20, 
        Language Language = Language.EN) : IRequest<ICollection<InventoryLogDetailsDto>>;
}