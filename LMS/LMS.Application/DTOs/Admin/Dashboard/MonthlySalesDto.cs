namespace LMS.Application.DTOs.Admin.Dashboard
{
    public class MonthlySalesDto
    {
        public int Year { get; set; }
        public int Month { get; set; }         
        public decimal TotalSales { get; set; }
    }
}
