using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.KPIs.GetPendingOrdersCount
{
    public record GetPendingOrdersCountQuery() : IRequest<int>;
}
