using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Domain.Enums.Commons;

namespace LMS.Application.Abstractions.Services.Admin
{
    public interface IDashboardHelper 
    {
        Task<ICollection<MonthlyOrdersDto>> GetMonthlyOrders(DateTime from, DateTime to);

        Task<ICollection<MonthlySalesDto>> GetMonthlySales(DateTime from, DateTime to);

        Task<ICollection<TopSellingProductDto>> GetTopSellingProducts(int topCoutn, Language language);
    }
}
