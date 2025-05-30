using System.Linq.Expressions;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Financial;

namespace LMS.Application.Specifications.Sales
{
    internal class RevenuesReportSpecification : ISpecification<FinancialRevenue>
    {
        public Expression<Func<FinancialRevenue, bool>>? Criteria { get; }


        public List<string> Includes => ["Employee", "Customer"];


        public bool IsTrackingEnabled => false;


        public Expression<Func<FinancialRevenue, object>>? OrderBy => null;


        public Expression<Func<FinancialRevenue, object>>? OrderByDescending =>
            financial => financial.CreatedAt;

        public int? Skip => null;

        public int? Take => null;

        public RevenuesReportSpecification(
            DateTime from,
            DateTime to)
        {
            Criteria = finnacial =>  from <= finnacial.CreatedAt && finnacial.CreatedAt <= to;
        }
    }
}
