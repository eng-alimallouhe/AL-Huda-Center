using LMS.Application.DTOs.Financial;
using LMS.Application.DTOs.Orders;

namespace LMS.Application.DTOs.Users
{
    public class CustomerDetailsDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime LastLogIn { get; set; }

        public ICollection<OrderOverviewDto> ViewOrders { get; set; }
        public ICollection<FinanicalOverviewDto> ViewFinancial { get; set; }
    }
}
