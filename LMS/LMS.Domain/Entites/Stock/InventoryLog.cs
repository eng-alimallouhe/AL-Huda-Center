using LMS.Domain.Enums.Stock;

namespace LMS.Domain.Entites.Stock
{
    public class InventoryLog
    {
        // Primary key:
        public Guid InventoryLogId { get; set; }


        //Foreign Key: ProductId ==> one(product)-to-many(inventoryLog) relationship
        public Guid ProductId { get; set; }


        public DateTime LogDate { get; set; }
        public LogType ChangeType { get; set; }
        public int ChangedQuantity { get; set; }


        public Product Product { get; set; }


        public InventoryLog()
        {
            InventoryLogId = Guid.NewGuid();
            Product = null!;
        }
    }
}
