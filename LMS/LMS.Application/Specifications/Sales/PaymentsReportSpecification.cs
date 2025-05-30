using System.Linq.Expressions;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Financial;

namespace LMS.Application.Specifications.Sales
{
    public class PaymentsReportSpecification : ISpecification<Payment>
    {
        public Expression<Func<Payment, bool>>? Criteria { get; }

        public List<string> Includes => ["Employee"];

        public bool IsTrackingEnabled => false;

        public Expression<Func<Payment, object>>? OrderBy => null;

        public Expression<Func<Payment, object>>? OrderByDescending =>
            payment => payment.Date;

        public int? Skip => null;

        public int? Take => null;


        public PaymentsReportSpecification(
            DateTime from,
            DateTime to)
        {
            Criteria = payment => from <= payment.Date && payment.Date <= to;
        }
    }
}
