namespace LMS.Domain.Entites.Orders
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