using LMS.Domain.Entities.Stock.Products;

namespace LMS.Domain.Entities.Stock
{
    public class Discount
    {
        // Primary key:
        public Guid DiscountId { get; set; }

        //Foreign Key: ProductId ==> one(product)-to-many(discount) relationship
        public Guid ProductId { get; set; }


        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;

        
        //Timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        // Navigation property:
        public Product Product { get; set; }


        public Discount()
        {
            DiscountId = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Product = null!;
        }
    }

}
