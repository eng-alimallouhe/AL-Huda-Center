using LMS.Domain.Enums.Commons;

namespace LMS.Domain.Entities.Stock.Products
{
    public class ProductTranslation
    {
        // Primary key:
        public Guid TranslationId { get; set; }


        //Foreign Key: ProductId ==> one(product)-to-many(translations) relationship
        public Guid ProductId { get; set; }


        public Language Language { get; set; }
        public required string ProductName { get; set; }
        public required string ProductDescription { get; set; }


        //Navigation Property:
        public Product Product { get; set; }


        public ProductTranslation()
        {
            TranslationId = Guid.NewGuid();
            Product = null!;
        }
    }
}
