using LMS.Domain.Entites.Orders;
using LMS.Domain.Entites.Stock.Categories;

namespace LMS.Domain.Entites.Stock.Products
{
    public class Product
    {
        // Primary key:
        public Guid ProductId { get; set; }


        public decimal ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public string ImgUrl { get; set; }


        //soft delete
        public bool IsActive { get; set; }


        //Timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        // Navigation property:
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Discount> Discounts { get; set; }
        public ICollection<InventoryLog> Logs { get; set; }
        public ICollection<ProductTranslation> Translations { get; set; }


        public Product()
        {
            ProductId = Guid.NewGuid();
            ImgUrl = string.Empty;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Categories = [];
            Discounts = [];
            Logs = [];
            Translations = [];
        }
    }
}