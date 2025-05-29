using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Application.Specifications.Admin;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Orders;
using MediatR;

namespace LMS.Application.Features.Admin.Sales.Queries.TopSalesProducts
{
    public class TopSalesProductsQueryHandler : IRequestHandler<TopSalesProductsQuery, ICollection<TopSellingProductDto>>
    {
        private readonly ISoftDeletableRepository<OrderItem> _orderItemsRepo;

        public TopSalesProductsQueryHandler(
            ISoftDeletableRepository<OrderItem> orderItemsRepo)
        {
            _orderItemsRepo = orderItemsRepo;
        }

        public async Task<ICollection<TopSellingProductDto>> Handle(TopSalesProductsQuery request, CancellationToken cancellationToken)
        {
            return await _orderItemsRepo.GetAllProjectedAsync(new TopSellingProductsSpecification(request.Number, request.Language));
        }
    }
}
