using LMS.Application.DTOs.Common;
using LMS.Application.DTOs.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Customers.Queries.GetInActiveCustomers
{
    public record GetInActiveCustomersQuery(
        DateTime From,
        int PageNumber,
        int PageSize) : IRequest<PagedResult<InActiveCustomersDto>>;
}
