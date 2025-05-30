using System.Linq.Expressions;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Stock.Products;

namespace LMS.Application.Specifications.Stock.Products
{
    public class StockValueSpecification : ISpecification<Product>
    {
        public Expression<Func<Product, bool>>? Criteria =>
            product => product.IsActive;

        public List<string> Includes => ["Logs", "Translations"];

        public bool IsTrackingEnabled => throw new NotImplementedException();

        public Expression<Func<Product, object>>? OrderBy => null;

        public Expression<Func<Product, object>>? OrderByDescending => 
            product => product.CreatedAt;

        public int? Skip => null;

        public int? Take => null;
    }
}
