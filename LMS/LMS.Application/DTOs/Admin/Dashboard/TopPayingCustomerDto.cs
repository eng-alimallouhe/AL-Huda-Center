namespace LMS.Application.DTOs.Admin.Dashboard
{
    public class TopPayingCustomerDto
    {
        public Guid CustomerId { get; set; }
        public string CustomerFullName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public decimal TotalSpent { get; set; }
    }
}
