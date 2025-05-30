namespace LMS.Application.DTOs.Admin.Sales
{
    public class PaymentDetailsDto
    {
        public Guid PaymentId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Details { get; set; }
    }
}
