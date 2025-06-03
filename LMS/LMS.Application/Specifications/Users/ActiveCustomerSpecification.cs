using System.Linq.Expressions;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Users;

namespace LMS.Application.Specifications.Users
{
    public class ActiveCustomerSpecification : ISpecification<Customer>
    {
        public Expression<Func<Customer, bool>>? Criteria =>   
            customer => !customer.IsDeleted;

        public List<string> Includes => ["Orders"];

        public bool IsTrackingEnabled => false;

        public Expression<Func<Customer, object>>? OrderBy => null;

        public Expression<Func<Customer, object>>? OrderByDescending =>
            customer => customer.LastLogIn;

        public int? Skip { get; }
        public int? Take { get; }

        public ActiveCustomerSpecification(
            int pageNumber,
            int pageSize)
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
