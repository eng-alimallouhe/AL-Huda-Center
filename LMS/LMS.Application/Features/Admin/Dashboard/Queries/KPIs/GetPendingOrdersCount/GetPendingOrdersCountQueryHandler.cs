using LMS.Application.Specifications.Admin;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Orders;
using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.KPIs.GetPendingOrdersCount
{
    public class GetPendingOrdersCountQueryHandler : IRequestHandler<GetPendingOrdersCountQuery, int>
    {
        private readonly ISoftDeletableRepository<BaseOrder> _baseOrderRepo;

        public GetPendingOrdersCountQueryHandler(
            ISoftDeletableRepository<BaseOrder> baseOrderRepo)
        {
            _baseOrderRepo = baseOrderRepo; 
        }


        public async Task<int> Handle(GetPendingOrdersCountQuery request, CancellationToken cancellationToken)
        {
            return (await _baseOrderRepo.GetAllAsync(new PendingOrdersSpecification())).Count();
        }
    }
}
