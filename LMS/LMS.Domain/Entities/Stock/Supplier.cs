namespace LMS.Domain.Entities.Stock
{
    public class Supplier
    {
        // Primary key:
        public Guid SupplierId { get; set; }


        public string SupplierName { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;


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
            Purchases = new List<Purchase>();
        }
    }
}
