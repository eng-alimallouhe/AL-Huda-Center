using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Domain.Enums.Commons;
using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.TopFiveSalesProducts
{
    public record TopFiveSalesProductsQuery(
        Language Language) : IRequest<ICollection<TopSellingProductDto>>;
}
