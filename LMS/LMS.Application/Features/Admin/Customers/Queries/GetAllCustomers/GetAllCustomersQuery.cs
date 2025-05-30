using LMS.Application.DTOs.Common;
using LMS.Application.DTOs.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Customers.Queries.GetAllCustomers
{
    public record GetAllCustomersQuery(
        int PageNumber,
        int PageSize) : IRequest<PagedResult<CustomersOverViewDto>>;
}
