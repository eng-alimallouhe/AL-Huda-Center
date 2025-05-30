namespace LMS.Application.DTOs.Users
{
    public class CustomersOverViewDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastLogIn { get; set; }
        public decimal TotalAmountSpent { get; set; }
    }
}