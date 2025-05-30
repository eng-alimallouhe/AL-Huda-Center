using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Financial;
using System.Linq.Expressions;

namespace LMS.Application.Specifications.Sales
{
    public class TopPayingCustomersSpecification
    : IProjectedSpecification<FinancialRevenue, TopPayingCustomerDto, Guid>
    {
        public Expression<Func<FinancialRevenue, bool>>? Criteria => null;

        public List<string> Includes => new()
        {
            "Customer"
        };

        public bool IsTrackingEnabled => false;

        public Expression<Func<FinancialRevenue, object>>? OrderBy => null;
        public Expression<Func<FinancialRevenue, object>>? OrderByDescending => null;

        public int? Skip { get; } = 0;
        public int? Take { get; }

        public TopPayingCustomersSpecification(int topCount)
        {
            Take = topCount;
        }

        public Expression<Func<FinancialRevenue, Guid>> GroupBy => r => r.CustomerId;

        public Expression<Func<IGrouping<Guid, FinancialRevenue>, TopPayingCustomerDto>> Selector =>
            group => new TopPayingCustomerDto
            {
                CustomerId = group.Key,
                CustomerFullName = group.Select(x => x.Customer.FullName).FirstOrDefault() ?? "N/A",
                TotalSpent = group.Sum(x => x.Amount)
            };

        public Func<IQueryable<TopPayingCustomerDto>, IOrderedQueryable<TopPayingCustomerDto>>? ResultOrdering =>
            query => query.OrderByDescending(x => x.TotalSpent);
    }
}