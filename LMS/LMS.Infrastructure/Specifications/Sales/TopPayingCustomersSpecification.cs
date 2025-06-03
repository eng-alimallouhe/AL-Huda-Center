using System.Linq.Expressions;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Orders;

namespace LMS.Infrastructure.Specifications.Sales
{
    public class TopPayingCustomersSpecification
    : IProjectedSpecification<BaseOrder, TopPayingCustomerDto, Guid>
    {
        public Expression<Func<BaseOrder, bool>>? Criteria => null;

        public List<string> Includes => new()
        {
            "Customer"
        };

        public bool IsTrackingEnabled => false;

        public Expression<Func<BaseOrder, object>>? OrderBy => null;
        public Expression<Func<BaseOrder, object>>? OrderByDescending => null;

        public int? Skip { get; } = 1;
        public int? Take { get; }

        public TopPayingCustomersSpecification(int topCount)
        {
            Take = topCount;
        }

        public Expression<Func<BaseOrder, Guid>> GroupBy => r => r.CustomerId;

        public Expression<Func<IGrouping<Guid, BaseOrder>, TopPayingCustomerDto>> Selector =>
            group => new TopPayingCustomerDto
            {
                CustomerId = group.Key,
                PhoneNumber = group.Max(x => x.Customer.PhoneNumber) ?? "N/A",
                CustomerFullName = group.Max(x => x.Customer.FullName) ?? "N/A",
                TotalSpent = group.Sum(x => x.Cost)
            };

        public Func<IQueryable<TopPayingCustomerDto>, IOrderedQueryable<TopPayingCustomerDto>>? ResultOrdering =>
            query => query.OrderByDescending(x => x.TotalSpent);
    }
}