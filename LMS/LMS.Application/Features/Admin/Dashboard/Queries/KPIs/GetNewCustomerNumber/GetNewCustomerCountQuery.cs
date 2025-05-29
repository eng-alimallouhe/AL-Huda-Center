using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.KPIs.GetNewCustomerNumber
{
    public record GetNewCustomerCountQuery(
        DateTime StartDate) : IRequest<int>;
}
