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



        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount => Quantity * UnitPrice * (DiscountPercentage / 100);
        public decimal PriceAfterDiscount => Quantity * UnitPrice - DiscountAmount;

        //Soft Delete:
        public bool IsActive { get; set; }


        //Timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        // Navigation property:
        public Order Order { get; set; }
        public Product Product { get; set; }

        public OrderItem()
        {
            OrderItemId = Guid.NewGuid();
            DiscountPercentage = 0;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Order = null!;
            Product = null!;
        }
    }
}
