using LMS.Application.DTOs.Admin.Dashboard;
using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.KPIs.GetUsersCount
{
    public record GetUsersCountQuery() : IRequest<NumberOfUsersDto>;
}
