using LMS.Application.DTOs.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Customers.Queries.GetNewCustomers
{
    public record GetNewCustomersQuery(
        DateTime StartDate) : IRequest<ICollection<CustomersOverViewDto>>;
}
