namespace LMS.Application.DTOs.Stock
{
    public class DeadStockDto
    {
        public string ProductName { get; set; }
        public int ProductStock { get; set; }
        public decimal ProductPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
