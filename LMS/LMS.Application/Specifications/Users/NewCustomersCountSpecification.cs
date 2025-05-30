using System.Linq.Expressions;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Users;

namespace LMS.Application.Specifications.Users
{
    public class NewCustomersCountSpecification : ISpecification<Customer>
    {
        public Expression<Func<Customer, bool>>? Criteria {  get; }

        public List<string> Includes => [];

        public bool IsTrackingEnabled => false;

        public Expression<Func<Customer, object>>? OrderBy => 
            book => book.CreatedAt;

        public Expression<Func<Customer, object>>? OrderByDescending => null;

        public int? Skip => null;

        public int? Take => null;


        public NewCustomersCountSpecification(
            DateTime startDate)
        {
            Criteria = customer => !customer.IsLocked && customer.CreatedAt >= startDate;
        }
    }
}
