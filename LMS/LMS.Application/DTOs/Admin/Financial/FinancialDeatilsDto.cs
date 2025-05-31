using LMS.Domain.Enums.Finacial;

namespace LMS.Application.DTOs.Admin.Financial
{
    public class FinancialDetailsDto
    {
        public Guid FinancialId { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
        public decimal Amount { get; set; }
        public Service Service { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
