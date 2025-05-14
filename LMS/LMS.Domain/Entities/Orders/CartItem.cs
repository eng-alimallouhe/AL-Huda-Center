using LMS.Domain.Entities.Stock;
using LMS.Domain.Entities.Stock.Products;

namespace LMS.Domain.Entities.Orders
{
    public class CartItem
    {
        // Primary key:
        public Guid CartItemId { get; set; }


        //Foreign Key: CartId ==> one(Cart)-to-many(CartItem) relationship
        public Guid CartId { get; set; }


        //Foreign Key: ProductId ==> one(Product)-to-many(CartItem) relationship
        public Guid ProductId { get; set; }
        
        
        //Foreign Key: DiscountId ==> one(discount)-to-one(CartItem) relationship
        public Guid? DiscounttId { get; set; }


        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal DiscountAmount  => Discount != null 
            ? Quantity * UnitPrice * (Discount.DiscountPercentage/100)
            : 0;

        public decimal TotalPrice => (Quantity * UnitPrice) - DiscountAmount;

        //Timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }



        // Navigation property:
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public Discount? Discount { get; set; }


        public CartItem()
        {
            CartItemId = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Cart = null!;
            Product = null!;
        }
    }
}
