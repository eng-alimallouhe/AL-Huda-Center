using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Domain.Enums.Commons;
using MediatR;

namespace LMS.Application.Features.Admin.Sales.Queries.TopSalesProducts
{
    public record TopSalesProductsQuery(
        int Number,
        Language Language) : IRequest<ICollection<TopSellingProductDto>>;
}
