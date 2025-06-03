using LMS.Application.Abstractions.Services.Admin;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Application.Specifications.Sales;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Orders;
using MediatR;

namespace LMS.Application.Features.Admin.Sales.Queries.TopSalesProducts
{
    public class TopSalesProductsQueryHandler : IRequestHandler<TopSalesProductsQuery, ICollection<TopSellingProductDto>>
    {
        private readonly IDashboardHelper _dashboradHelper;

        public TopSalesProductsQueryHandler(
            IDashboardHelper dashboardHelper)
        {
            _dashboradHelper = dashboardHelper;
        }

        public async Task<ICollection<TopSellingProductDto>> Handle(TopSalesProductsQuery request, CancellationToken cancellationToken)
        {
            return await _dashboradHelper.GetTopSellingProducts(request.Number, request.Language);
        }
    }
}
