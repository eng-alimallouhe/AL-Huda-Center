using LMS.Application.Abstractions.Services.Admin;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Orders;
using LMS.Infrastructure.Specifications.Sales;

namespace LMS.Infrastructure.Services.Admin
{
    public class Saleshelper : ISalesHelper
    {
        private readonly ISoftDeletableRepository<BaseOrder> _baseOrderRepo;

        public Saleshelper(
            ISoftDeletableRepository<BaseOrder> baseOrderRepo)
        {
            _baseOrderRepo = baseOrderRepo;
        }

        public Task<ICollection<TopPayingCustomerDto>> GetTopPayingCustomersAsync(int topCount)
        {
            var response = _baseOrderRepo.GetAllProjectedAsync(new TopPayingCustomersSpecification(topCount));
            
            return response;
        }
    }
}
