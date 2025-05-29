using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Application.Specifications.Admin;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Orders;
using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.MonthlySales
{
    public class MonthlySalesQueryHandler : IRequestHandler<MonthlySalesQuery, ICollection<MonthlySalesDto>>
    {
        private readonly ISoftDeletableRepository<BaseOrder> _baseOrderRepo;

        public MonthlySalesQueryHandler(
            ISoftDeletableRepository<BaseOrder> baseOrderRepo)
        {
            _baseOrderRepo = baseOrderRepo;
        }

        public async Task<ICollection<MonthlySalesDto>> Handle(MonthlySalesQuery request, CancellationToken cancellationToken)
        {
            return await _baseOrderRepo.GetAllProjectedAsync(new MonthlySalesFromBaseOrdersSpecification(request.From, request.To));
        }
    }
}
