using System.Linq.Expressions;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Orders;

namespace LMS.Application.Specifications.Orders
{
    public class PendingOrdersSpecification : ISpecification<BaseOrder>
    {
        public Expression<Func<BaseOrder, bool>>? Criteria { get;  }

        public List<string> Includes => [];

        public bool IsTrackingEnabled => false;

        public Expression<Func<BaseOrder, object>>? OrderBy => null;

        public Expression<Func<BaseOrder, object>>? OrderByDescending =>
                order => order.CreatedAt;

        public int? Skip => null;

        public int? Take => null;

        public PendingOrdersSpecification()
        {
            Criteria = order => order.Status == Domain.Enums.Orders.OrderStatus.Pending;
        }
    }
}
