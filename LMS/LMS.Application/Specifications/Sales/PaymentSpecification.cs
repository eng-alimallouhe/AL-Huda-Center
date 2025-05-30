using System.Linq.Expressions;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Financial;

namespace LMS.Application.Specifications.Sales
{
    public class PaymentSpecification : ISpecification<Payment>
    {
            public Expression<Func<Payment, bool>>? Criteria => 
                payment => payment.IsActive;

            public List<string> Includes => ["Employee"];

            public bool IsTrackingEnabled => false;

            public Expression<Func<Payment, object>>? OrderBy => null;

            public Expression<Func<Payment, object>>? OrderByDescending => 
                payment => payment.Date;

            public int? Skip { get; }

            public int? Take { get; }


        public PaymentSpecification(int pageNumber, int pageSize)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                Skip = 1;
                Take = 1;
            }
            else
            {
                Skip = pageNumber;
                Take = pageSize;
            }
        }
    }
}
