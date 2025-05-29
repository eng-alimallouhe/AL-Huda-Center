using System.Linq.Expressions;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Stock.Products;

namespace LMS.Application.Specifications.Admin
{
    public class ActiveBookSpecification : ISpecification<Book>
    {
        public Expression<Func<Book, bool>>? Criteria {  get; }

        public List<string> Includes => [];

        public bool IsTrackingEnabled => false;

        public Expression<Func<Book, object>>? OrderBy => 
            book => book.CreatedAt;

        public Expression<Func<Book, object>>? OrderByDescending => null;

        public int? Skip => null;

        public int? Take => null;


        public ActiveBookSpecification(
            DateTime? startDate)
        {
            if (startDate.HasValue)
            {
                Criteria = book => book.IsActive && book.CreatedAt >= startDate.Value;
            }
            else
            {
                Criteria = book => book.IsActive;
            }
        }
    }
}
