using LMS.Application.DTOs.Admin.Dashboard;
using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.MonthlyOrders
{
    public record MonthlyOrdersQuery(
        DateTime From,
        DateTime To) : IRequest<ICollection<MonthlyOrdersDto>>;
}