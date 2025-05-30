using System.Linq.Expressions;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Users;

namespace LMS.Application.Specifications.Users
{
    public class SingleCustomerSpecification : ISpecification<Customer>
    {


        public Expression<Func<Customer, bool>>? Criteria { get; }

        public List<string> Includes => ["FinancialRevenues", "Orders"];

        public bool IsTrackingEnabled => false;

        public Expression<Func<Customer, object>>? OrderBy => null;

        public Expression<Func<Customer, object>>? OrderByDescending => null;

        public int? Skip => null;

        public int? Take => null;
        
        public SingleCustomerSpecification(Guid userId)
        {
            Criteria = user => user.UserId == userId;
        }
    }
}
