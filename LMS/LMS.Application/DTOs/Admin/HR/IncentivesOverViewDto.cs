namespace LMS.Application.DTOs.Admin.HR
{
    public class IncentivesOverViewDto
    {
        public decimal Amount { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Date { get; set; }
        public string IsDisbursed { get; set; }
    }
}
