using LMS.Application.DTOs.Admin.Dashboard;
using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.MonthlySales
{
    public record MonthlySalesQuery(
        DateTime From,
        DateTime To) : IRequest<ICollection<MonthlySalesDto>>;
}
