using System.Linq.Expressions;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Stock.Products;

namespace LMS.Application.Specifications.Stock.Products
{
    public class DeadStockSpecification : ISpecification<Product>
    {
        private readonly DateTime _from;


        public Expression<Func<Product, bool>>? Criteria => 
            product => !product.OrderItems.Any() || 
                        !product.OrderItems.Any(orderItem => orderItem.CreatedAt >= _from);

        public List<string> Includes => ["OrderItems", "Translations"];

        public bool IsTrackingEnabled => false;

        public Expression<Func<Product, object>>? OrderBy => null;
            
        public Expression<Func<Product, object>>? OrderByDescending => null;

        public int? Skip { get; }
        public int? Take  { get; }

        public DeadStockSpecification(
            DateTime from,
            int? pageNumber,
            int? pageSize)
        {
            _from = from;

            if (pageNumber.HasValue && pageSize.HasValue)
            {

                if (pageNumber < 1 || pageSize < 1)
                {
                    Skip = 1;
                    Take = 10;
                }
                else
                {
                    Skip = pageNumber;
                    Take = pageSize;
                }
            }
        }
    }
}
