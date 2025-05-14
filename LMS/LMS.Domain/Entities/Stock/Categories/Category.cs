using LMS.Domain.Entities.Stock.Products;

namespace LMS.Domain.Entities.Stock.Categories
{
    public class Category
    {
        //Primary Key:
        public Guid CategoryId { get; set; }


        //soft delete: 
        public bool IsActive { get; set; }


        //timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        //Navigation Property:
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
