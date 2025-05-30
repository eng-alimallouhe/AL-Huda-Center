using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Orders;
using System.Linq.Expressions;

namespace LMS.Application.Specifications.Sales
{
    public class MonthlySalesFromBaseOrdersSpecification
        : IProjectedSpecification<BaseOrder, MonthlySalesDto, (int Year, int Month)>
    {
        public Expression<Func<BaseOrder, bool>>? Criteria { get; }

        public List<string> Includes => new();

        public bool IsTrackingEnabled => false;

        public Expression<Func<BaseOrder, object>>? OrderBy => null;
        public Expression<Func<BaseOrder, object>>? OrderByDescending => null;

        public int? Skip => null;
        public int? Take => null;

        public Expression<Func<BaseOrder, (int Year, int Month)>> GroupBy =>
                order => new ValueTuple<int, int>(order.CreatedAt.Year, order.CreatedAt.Month);

        public Expression<Func<IGrouping<(int Year, int Month), BaseOrder>, MonthlySalesDto>> Selector =>
            group => new MonthlySalesDto
            {
                Year = group.Key.Year,
                Month = group.Key.Month,
                TotalSales = group.Count()
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