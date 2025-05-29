namespace LMS.Application.DTOs.Admin.Dashboard
{
    public class StockInfromationDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductStock { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
