using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LMS.Infrastructure.Specifications.Sales
{
    public class MonthlySalesFromBaseOrdersSpecification
        : IProjectedSpecification<BaseOrder, MonthlySalesDto, object> // ← تغيير نوع المفتاح لـ object
    {
        public Expression<Func<BaseOrder, bool>>? Criteria { get; }

        public List<string> Includes => new();

        public bool IsTrackingEnabled => false;

        public Expression<Func<BaseOrder, object>>? OrderBy => null;
        public Expression<Func<BaseOrder, object>>? OrderByDescending => null;

        public int? Skip => null;
        public int? Take => null;

        public Expression<Func<BaseOrder, object>> GroupBy =>
            order => new { order.CreatedAt.Year, order.CreatedAt.Month };

        public Expression<Func<IGrouping<object, BaseOrder>, MonthlySalesDto>> Selector =>
            group => new MonthlySalesDto
            {
                Year = EF.Property<int>(group.Key, "Year"),
                Month = EF.Property<int>(group.Key, "Month"),
                TotalSales = group.Sum(order => order.Cost)
            };

        public Func<IQueryable<MonthlySalesDto>, IOrderedQueryable<MonthlySalesDto>>? ResultOrdering =>
            query => query.OrderByDescending(x => x.TotalSales);

        public MonthlySalesFromBaseOrdersSpecification(
            DateTime from,
            DateTime to)
        {
            Criteria = o => o.CreatedAt >= from && o.CreatedAt <= to;
        }
    }
}
