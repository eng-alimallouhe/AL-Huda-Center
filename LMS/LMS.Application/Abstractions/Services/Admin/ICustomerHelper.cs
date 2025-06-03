using LMS.Application.DTOs.Admin.Dashboard;

namespace LMS.Application.Abstractions.Services.Admin
{
    public interface ISalesHelper
    {
        Task<ICollection<TopPayingCustomerDto>> GetTopPayingCustomersAsync(int topCount);
    }
}
