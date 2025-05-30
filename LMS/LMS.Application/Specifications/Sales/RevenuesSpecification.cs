using System.Linq.Expressions;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Financial;

namespace LMS.Application.Specifications.Sales
{
    public class RevenuesSpecification : ISpecification<FinancialRevenue>
    {
        public Expression<Func<FinancialRevenue, bool>>? Criteria => 
            financial => financial.IsActive;

        public List<string> Includes => ["Employee", "Customer"];

        public bool IsTrackingEnabled => false;

        public Expression<Func<FinancialRevenue, object>>? OrderBy => null;  

        public Expression<Func<FinancialRevenue, object>>? OrderByDescending =>
            financial => financial.CreatedAt;


        public int? Skip { get; }

        public int? Take { get; }

        public RevenuesSpecification(
            int? skip,
            int? take)
        {
            Skip = skip;
            Take = take;
        }
    }
}
