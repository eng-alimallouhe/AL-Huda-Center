using System.Linq.Expressions;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Orders;
using LMS.Domain.Enums.Commons;

namespace LMS.Application.Specifications.Admin
{
    public class TopSellingProductsSpecification : IProjectedSpecification<OrderItem, TopSellingProductDto, Guid>
    {
        public Expression<Func<OrderItem, bool>>? Criteria => null;

        public List<string> Includes => new()
        {
            "Product"
        };

        public bool IsTrackingEnabled => false;

        public Expression<Func<OrderItem, object>>? OrderBy => null;

        public Expression<Func<OrderItem, object>>? OrderByDescending => null;

        public int? Skip { get; } = 0;

        public int? Take { get; }

        Language Language;



        // ← Group by ProductId
        public Expression<Func<OrderItem, Guid>> GroupBy => oi => oi.ProductId;


        // ← Select projected result
        public Expression<Func<IGrouping<Guid, OrderItem>, TopSellingProductDto>> Selector =>
            group => new TopSellingProductDto
            {
                ProductId = group.Key,
                ProductName = group.Select(x => x.Product.Translations.Where(pt => pt.Language == Language).FirstOrDefault()!.ProductName ?? "N/A").FirstOrDefault()!,
                TotalSold = group.Sum(x => x.Quantity)
            };


        public Func<IQueryable<TopSellingProductDto>, IOrderedQueryable<TopSellingProductDto>>? ResultOrdering =>
            query => query.OrderByDescending(x => x.TotalSold);

        public TopSellingProductsSpecification(int topCount, Language language)
        {
            Language = language;
            Take = topCount;
        }
    }
}