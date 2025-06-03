using LMS.Application.Abstractions.Services.Admin;
using LMS.Application.DTOs.Admin.Dashboard;
using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.TopFiveSalesProducts
{
    public class TopFiveSalesProductsQueryHandler : IRequestHandler<TopFiveSalesProductsQuery, ICollection<TopSellingProductDto>>
    {
        private readonly IDashboardHelper _dashboardHelper;

        public TopFiveSalesProductsQueryHandler(
            IDashboardHelper dashboardHelper)
        {
            _dashboardHelper = dashboardHelper;
        }

        public async Task<ICollection<TopSellingProductDto>> Handle(TopFiveSalesProductsQuery request, CancellationToken cancellationToken)
        {
            return await _dashboardHelper.GetTopSellingProducts(5, request.Language);
        }
    }
}