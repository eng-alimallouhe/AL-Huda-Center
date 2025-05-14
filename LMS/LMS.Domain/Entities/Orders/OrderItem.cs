using LMS.Domain.Entities.Stock;
using LMS.Domain.Entities.Stock.Products;

namespace LMS.Domain.Entities.Orders
{
    public class OrderItem
    {
        // Primary key:
        public Guid OrderItemId { get; set; }

        //Foreign Key: SellOrderId ==> one(SellOrder)-to-many(OrderItem) relationship
        public Guid SellOrderId { get; set; }

        //Foreign Key: ProductId ==> one(Product)-to-many(OrderItem) relationship
        public Guid ProductId { get; set; }


        //Foreign Key: DiscounttId ==> one(discount)-to-one(orderItem) relationship
        public Guid? DiscountId { get; set; }


        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount => Discount != null
            ? Quantity * UnitPrice * (Discount.DiscountPercentage / 100)
            : 0;

        public decimal TotalPrice => (Quantity * UnitPrice) - DiscountAmount;

        
        //Soft Delete:
        public bool IsActive { get; set; }

        
        //Timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        // Navigation property:
        public SellOrder SellOrder { get; set; }
        public Product Product { get; set; }
        public Discount? Discount { get; set; }

        public OrderItem()
        {
            OrderItemId = Guid.NewGuid();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            SellOrder = null!;
            Product = null!;
        }
    }
}