using System.Linq.Expressions;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Users;

namespace LMS.Application.Specifications.Users
{
    public class InActiveCustomersSpecification : ISpecification<Customer>
    {
        private readonly DateTime _from;


        public Expression<Func<Customer, bool>>? Criteria =>
               customer => !customer.Orders.Any()
               || !customer.Orders.Any(order => order.CreatedAt >= _from);
        
        public List<string> Includes => ["Orders"];

        public bool IsTrackingEnabled => false;

        public Expression<Func<Customer, object>>? OrderBy => null;

        public Expression<Func<Customer, object>>? OrderByDescending =>
            customer => customer.Orders.Min(order => order.CreatedAt);
        
        public int? Skip { get; }
        public int? Take { get; }

        public InActiveCustomersSpecification(
            DateTime from,
            int pageNumber,
            int pageSize)
        {
            _from = from;
            if (pageNumber < 1 || pageSize < 1)
            {
                Take = 1;
                Skip = 1;
            }
            else
            {
                Skip = pageNumber;
                Take = pageSize;
            }
        }
    }
}
