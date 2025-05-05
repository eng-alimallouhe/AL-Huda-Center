namespace LMS.Domain.Entites.Stock
{
    public class Category
    {
        //Primary key:
        public Guid CategoryId { get; set; }


        public required string CategoryName { get; set; }
        public required string CategoryDescription { get; set; }

        
        //soft delete: 
        public bool IsActive { get; set; }


        //timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        //Navigation property:
        public ICollection<Product> Products { get; set; }


        public Category()
        {
            CategoryId = Guid.NewGuid();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Products = [];
        }
    }
}
