using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.KPIs.GetNewBooksCount
{
    public record GetNewBooksCountQuery(
        DateTime StartDate) : IRequest<int>;
}
