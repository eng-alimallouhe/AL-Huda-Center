using LMS.Domain.Enums.Orders;

namespace LMS.Application.DTOs.Orders
{
    public class OrderOverviewDto
    {
        public Guid OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}