namespace LMS.Domain.Entites.Stock
{
    public class Supplier
    {
        // Primary key:
        public Guid SupplierId { get; set; }


        public required string SupplierName { get; set; }
        public required string ContactPhone { get; set; }
        public string? ContactEmail { get; set; }

        
        //soft delete
        public bool IsActive { get; set; }

        
        //Timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

      
        // Navigation property:
        public ICollection<Purchase> Purchases { get; set; }


        public Supplier()
        {
            SupplierId = Guid.NewGuid();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Purchases = [];
        }
    }
}
