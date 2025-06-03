using LMS.Application.Abstractions.Services.Admin;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Orders;
using LMS.Domain.Enums.Commons;
using LMS.Infrastructure.Specifications.Orders;
using LMS.Infrastructure.Specifications.Sales;

namespace LMS.Infrastructure.Services.Admin
{
    public class DashboardHelper : IDashboardHelper
    {
        private readonly ISoftDeletableRepository<BaseOrder> _baseOrderRepo;
        private readonly ISoftDeletableRepository<OrderItem> _orderItemRepo;


        public DashboardHelper(
            ISoftDeletableRepository<BaseOrder> baseOrderRepo,
            ISoftDeletableRepository<OrderItem> orderItemRepo)
        {
            _baseOrderRepo = baseOrderRepo;
            _orderItemRepo = orderItemRepo;
        }


        public async Task<ICollection<MonthlyOrdersDto>> GetMonthlyOrders(DateTime from, DateTime to)
        {
            var response = await _baseOrderRepo.GetAllProjectedAsync(new MonthlyOrdersSpecification(from, to));
            return response;
        }

        public async Task<ICollection<MonthlySalesDto>> GetMonthlySales(DateTime from, DateTime to)
        {
            var response = await _baseOrderRepo.GetAllProjectedAsync(new MonthlySalesFromBaseOrdersSpecification(from, to));
            return response;
        }

        public async Task<ICollection<TopSellingProductDto>> GetTopSellingProducts(int topCoutn, Language language)
        {
            var response = await _orderItemRepo.GetAllProjectedAsync(new TopSellingProductsSpecification(topCoutn, language));
            return response;
        }
    }
}
