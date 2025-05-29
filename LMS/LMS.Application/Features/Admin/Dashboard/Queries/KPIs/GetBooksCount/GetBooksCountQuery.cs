using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.KPIs.GetBooksCount
{
    public record GetBooksCountQuery() :IRequest<int>;
}
