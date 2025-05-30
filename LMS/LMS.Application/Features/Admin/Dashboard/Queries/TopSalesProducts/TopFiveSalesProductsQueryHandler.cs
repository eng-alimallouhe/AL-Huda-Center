using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Application.Specifications.Sales;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Orders;
using MediatR;

namespace LMS.Application.Features.Admin.Dashboard.Queries.TopFiveSalesProducts
{
    public class TopFiveSalesProductsQueryHandler : IRequestHandler<TopFiveSalesProductsQuery, ICollection<TopSellingProductDto>>
    {
        private readonly ISoftDeletableRepository<OrderItem> _orderItemsRepo;

        public TopFiveSalesProductsQueryHandler(
            ISoftDeletableRepository<OrderItem> orderItemsRepo)
        {
            _orderItemsRepo = orderItemsRepo;
        }

        public async Task<ICollection<TopSellingProductDto>> Handle(TopFiveSalesProductsQuery request, CancellationToken cancellationToken)
        {
            return await _orderItemsRepo.GetAllProjectedAsync(new TopSellingProductsSpecification(5, request.Language));
        }
    }
}