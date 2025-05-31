using System.Linq.Expressions;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Stock.Products;

namespace LMS.Application.Specifications.Stock.Products
{
    public class StockSnapshotSpecification : ISpecification<Product>
    {
        public Expression<Func<Product, bool>>? Criteria =>     
                product => product.IsActive;
        public List<string> Includes => ["Translations", "Logs"];

        public bool IsTrackingEnabled => false;

        public Expression<Func<Product, object>>? OrderBy => null;

        public Expression<Func<Product, object>>? OrderByDescending => 
            product => product.CreatedAt;

        public int? Skip {get; }

        public int? Take { get; }

        public StockSnapshotSpecification(
            int? pageNumber,
            int? pageSize)
        {
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                if (pageNumber < 1 || pageSize < 1)
                {
                    Skip = 1;
                    Take = 1;
                }
                else
                {
                    Skip = pageNumber.Value;
                    Take = pageSize.Value;
                }
            }
            else
            {
                Skip = null;
                Take = null;
            }
        }
    }
}
