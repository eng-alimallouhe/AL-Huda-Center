using LMS.Application.DTOs.Admin.Dashboard;
using MediatR;

namespace LMS.Application.Features.Admin.Customers.Queries.TopPayingCustomers
{
    public record TopPayingCustomersQuery(
        int TopCount) : IRequest<ICollection<TopPayingCustomerDto>>;
}