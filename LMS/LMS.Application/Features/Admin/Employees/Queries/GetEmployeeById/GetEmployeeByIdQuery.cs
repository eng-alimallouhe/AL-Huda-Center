using LMS.Application.DTOs.Admin.Employees;
using MediatR;

namespace LMS.Application.Features.Admin.Employees.Queries.GetEmployeeById
{
    public record GetEmployeeByIdQuery(Guid Id) : IRequest<EmployeeDetailsDto?>;
}