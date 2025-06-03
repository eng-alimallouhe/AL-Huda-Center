namespace LMS.Application.DTOs.Admin.HR
{
    public class PenaltiesOverviewDto
    {
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public string Date { get; set; }
        public string IsDeducted { get; set; }
    }
}
