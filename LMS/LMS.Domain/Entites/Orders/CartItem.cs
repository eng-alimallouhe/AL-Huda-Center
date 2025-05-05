using LMS.Domain.Entites.Stock;
using LMS.Domain.Entites.Stock.Products;

namespace LMS.Domain.Entites.Orders
{
    public class CartItem
    {
        // Primary key:
        public Guid CartItemId { get; set; }


        //Foreign Key: CartId ==> one(cart)-to-many(cartItem) relationship
        public Guid CartId { get; set; }


        //Foreign Key: ProductId ==> one(product)-to-many(cartItem) relationship
        public Guid ProductId { get; set; }


        //Foreign Key: ProductId ==> one(discount)-to-one(cartItem) relationship
        public Guid? DiscountId { get; set; }



        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
       
        public decimal DiscountAmount => 
            Discount != null
            ? Quantity * UnitPrice * (Discount.DiscountPercentage / 100)
            : 0;

        public decimal PriceAfterDiscount => TotalPrice - DiscountAmount;

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
