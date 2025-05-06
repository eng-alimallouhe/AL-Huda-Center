namespace LMS.Domain.Entities.Orders
{
    public class Order : BaseOrder
    {
        // Navigation property:
        public ICollection<OrderItem> OrderItems { get; set; }


        public Order()
        {
            OrderItems = [];
        }
    }
}