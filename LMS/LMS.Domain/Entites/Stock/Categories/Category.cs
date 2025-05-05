using LMS.Domain.Entites.Stock.Products;

namespace LMS.Domain.Entites.Stock.Categories
{
    public class Category
    {
        //Primary key:
        public Guid CategoryId { get; set; }

        
        //soft delete: 
        public bool IsActive { get; set; }


        //timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        //Navigation property:
        public ICollection<Product> Products { get; set; }
        public ICollection<CategoryTranslation> Translations { get; set; }


        public Category()
        {
            CategoryId = Guid.NewGuid();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Products = [];
            Translations = [];
        }
    }
}
