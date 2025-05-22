namespace LMS.Application.DTOs.Financial
{
    public class FinanicalOverviewDto
    {
        public Guid FinancialRevenueId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
