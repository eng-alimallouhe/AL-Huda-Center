using System.Linq.Expressions;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Orders;

namespace LMS.Application.Specifications.Orders
{
    public class MonthlyOrdersSpecification : IProjectedSpecification<BaseOrder, MonthlyOrdersDto, (int Year, int Month)>
    {
        public Expression<Func<BaseOrder, (int Year, int Month)>> GroupBy =>
                order => new ValueTuple<int, int>(order.CreatedAt.Year, order.CreatedAt.Month);

        public Expression<Func<IGrouping<(int Year, int Month), BaseOrder>, MonthlyOrdersDto>> Selector =>
            group => new MonthlyOrdersDto
            {
                Year = group.Key.Year,
                Month = group.Key.Month,
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

        public MonthlyOrdersSpecification(
            DateTime from, 
            DateTime to)
        {
            Criteria = order => order.CreatedAt >= from && order.CreatedAt <= to;
        }
    }
}
