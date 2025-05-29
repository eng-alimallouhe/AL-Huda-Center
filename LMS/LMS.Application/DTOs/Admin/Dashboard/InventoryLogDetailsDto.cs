using LMS.Domain.Enums.Stock;

namespace LMS.Application.DTOs.Admin.Dashboard
{
    public class InventoryLogDetailsDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime LogDate { get; set; }
        public LogType ChangeType { get; set; }
    }
}
