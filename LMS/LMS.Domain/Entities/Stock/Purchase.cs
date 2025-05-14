namespace LMS.Domain.Entities.Stock
{
    public class Purchase
    {
        // Primary key:
        public Guid PurchaseId { get; set; }

        
        //Foreign Key: SupplierId ==> one(Supplier)-to-many(Purchase) relationship
        public Guid SupplierId { get; set; }


        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string CurrencyCode { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;


        //soft delete
        public bool IsActive { get; set; }


        //Timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        // Navigation property:
        public Supplier Supplier { get; set; }


        public Purchase()
        {
            PurchaseId = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsActive = true;
            Supplier = null!;
        }
    }

}
