namespace LMS.Application.DTOs.Users
{
    public class InActiveCustomersDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastOrderDate { get; set; }
    }
}