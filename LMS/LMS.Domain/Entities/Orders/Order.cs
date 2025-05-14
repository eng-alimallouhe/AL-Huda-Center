namespace LMS.Domain.Entities.Orders
{
    public class Order : BaseOrder
    {
        // Navigation property:
        public ICollection<OrderItem> OrderItems { get; set; }

        
        public decimal TotalCost => OrderItems.Sum(o => o.TotalPrice);


        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }
    }
}
