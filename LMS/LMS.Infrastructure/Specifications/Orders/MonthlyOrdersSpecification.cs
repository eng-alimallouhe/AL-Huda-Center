using System.Linq.Expressions;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Specifications.Orders
{
    public class MonthlyOrdersSpecification : IProjectedSpecification<BaseOrder, MonthlyOrdersDto, object>
    {
        public Expression<Func<BaseOrder, object>> GroupBy =>
                order => new { order.CreatedAt.Year, order.CreatedAt.Month };

        public Expression<Func<IGrouping<object, BaseOrder>, MonthlyOrdersDto>> Selector =>
            group => new MonthlyOrdersDto
            {
                Year = EF.Property<int>(group.Key, "Year"),
                Month = EF.Property<int>(group.Key, "Month"),
                TotalOrdersCount = group.Count()
            };

        public Func<IQueryable<MonthlyOrdersDto>, IOrderedQueryable<MonthlyOrdersDto>>? ResultOrdering =>
            query => query.OrderByDescending(x => x.Month);

        public Expression<Func<BaseOrder, bool>>? Criteria { get; }

        public List<string> Includes => [];

        public bool IsTrackingEnabled => false;

        public Expression<Func<BaseOrder, object>>? OrderBy => null;

        public Expression<Func<BaseOrder, object>>? OrderByDescending => null;

        public int? Skip => null;

        public int? Take => null;

        public MonthlyOrdersSpecification(DateTime from, DateTime to)
        {
            Criteria = order => 
                    order.CreatedAt >= from && order.CreatedAt <= to && 
                    order.PaymentStatus == Domain.Enums.Orders.PaymentStatus.Paid;
        }
    }
}
