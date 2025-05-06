namespace LMS.Domain.Entites.Stock.Products
{
    public class ProductTranslation
    {
        //Primary key:
        public Guid TranslationId { get; set; }


        //Foreign key: ProductId ==> one(product)--to--many(productTranslation)
        public Guid ProductId { get; set; }


        public required string Language { get; set; }
        public required string ProductName { get; set; }
        public required string ProductDescription { get; set; }


        //Navigation properties:
        public Product Product { get; set; }


        public ProductTranslation()
        {
            TranslationId = Guid.NewGuid();
            Product = null!;
        }
    }
}
