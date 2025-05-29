namespace LMS.Application.DTOs.Admin.Dashboard
{
    public class MonthlyOrdersDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int TotalOrdersCount { get; set; }
    }
}
