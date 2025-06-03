using LMS.Application.Abstractions.Services.Admin;
using LMS.Application.DTOs.Admin.Dashboard;
using MediatR;

namespace LMS.Application.Features.Admin.Customers.Queries.TopPayingCustomers
{
    public class TopPayingCustomersQueryHandler : IRequestHandler<TopPayingCustomersQuery, ICollection<TopPayingCustomerDto>>
    {
        private readonly ISalesHelper _salesHelper;

        public TopPayingCustomersQueryHandler(
            ISalesHelper salesHelper)
        {
            _salesHelper = salesHelper;
        }

        public async Task<ICollection<TopPayingCustomerDto>> Handle(TopPayingCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _salesHelper.GetTopPayingCustomersAsync(request.TopCount);
        }
    }
}
