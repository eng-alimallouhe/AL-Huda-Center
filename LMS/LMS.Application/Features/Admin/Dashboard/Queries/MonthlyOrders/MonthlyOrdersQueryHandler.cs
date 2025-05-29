using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Application.Specifications.Admin;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Orders;
using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.MonthlyOrders
{
    public class MonthlyOrdersQueryHandler : IRequestHandler<MonthlyOrdersQuery, ICollection<MonthlyOrdersDto>>
    {
        private readonly ISoftDeletableRepository<BaseOrder> _baseOrderRepo;

        public MonthlyOrdersQueryHandler(
            ISoftDeletableRepository<BaseOrder> baseOrderRepo)
        {
            _baseOrderRepo = baseOrderRepo;
        }

        public async Task<ICollection<MonthlyOrdersDto>> Handle(MonthlyOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _baseOrderRepo.GetAllProjectedAsync(new MonthlyOrdersSpecification(request.From, request.To));
        }
    }
}
