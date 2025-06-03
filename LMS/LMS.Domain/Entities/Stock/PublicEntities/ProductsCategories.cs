using LMS.Domain.Entities.Stock.Categories;
using LMS.Domain.Entities.Stock.Products;

namespace LMS.Domain.Entities.Stock.PublicEntities
{
    public class ProductCategory
    {
        public Guid ProductCategoryId { get; set; }

        public Guid ProductId { get; set; }

        public Guid CategoryId { get; set; }


        public Product Product { get; set; }

        public Category Category { get; set; }

        public ProductCategory()
        {
            ProductCategoryId = Guid.NewGuid();
            Product = null!;
            Category = null!;
        }
    }
}
