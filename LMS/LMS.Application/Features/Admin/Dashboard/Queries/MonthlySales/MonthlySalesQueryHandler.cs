using LMS.Application.Abstractions.Services.Admin;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Application.Specifications.Sales;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Orders;
using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.MonthlySales
{
    public class MonthlySalesQueryHandler : IRequestHandler<MonthlySalesQuery, ICollection<MonthlySalesDto>>
    {
        private readonly IDashboardHelper _dashboardHelper;

        public MonthlySalesQueryHandler(
            IDashboardHelper dashboardHelper)
        {
            _dashboardHelper = dashboardHelper;
        }

        public async Task<ICollection<MonthlySalesDto>> Handle(MonthlySalesQuery request, CancellationToken cancellationToken)
        {
            return await _dashboardHelper.GetMonthlySales(request.From, request.To);
        }
    }
}
