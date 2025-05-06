namespace LMS.Domain.Entities.Stock
{
    public class Purchase
    {
        // Primary key:
        public Guid PurchaseId { get; set; }


        //Foreign Key: SupplierId ==> one(supplier)-to-many(purchase) relationship
        public Guid SupplierId { get; set; }


        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public required string CurrencyCode { get; set; }
        public string? Notes { get; set; }
        public string PurchaseImgUrl { get; set; }


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
            PurchaseImgUrl = string.Empty;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsActive = true;
            Supplier = null!;
        }
    }
}
