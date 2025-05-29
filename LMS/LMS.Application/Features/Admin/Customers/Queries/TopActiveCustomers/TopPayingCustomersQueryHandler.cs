using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Application.Specifications.Admin;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Financial;
using MediatR;

namespace LMS.Application.Features.Admin.Customers.Queries.TopPayingCustomers
{
    public class TopPayingCustomersQueryHandler : IRequestHandler<TopPayingCustomersQuery, ICollection<TopPayingCustomerDto>>
    {
        private readonly ISoftDeletableRepository<FinancialRevenue> _financialRepo;

        public TopPayingCustomersQueryHandler(
            ISoftDeletableRepository<FinancialRevenue> financialRepo)
        {
            _financialRepo = financialRepo;
        }

        public async Task<ICollection<TopPayingCustomerDto>> Handle(TopPayingCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _financialRepo.GetAllProjectedAsync(new TopPayingCustomersSpecification(request.TopCount));
        }
    }
}
