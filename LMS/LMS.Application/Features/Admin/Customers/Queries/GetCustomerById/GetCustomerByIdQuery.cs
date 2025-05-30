using LMS.Application.DTOs.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Customers.Queries.GetCustomerById
{
    public record GetCustomerByIdQuery(
        Guid userId) : IRequest<CustomerDetailsDto?>;
}
