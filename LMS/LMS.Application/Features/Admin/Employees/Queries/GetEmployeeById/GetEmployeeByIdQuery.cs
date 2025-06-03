using LMS.Application.DTOs.Admin.Employees;
using LMS.Domain.Enums.Commons;
using MediatR;

namespace LMS.Application.Features.Admin.Employees.Queries.GetEmployeeById
{
    public record GetEmployeeByIdQuery(
        Guid Id,
        Language Language) : IRequest<EmployeeDetailsDto?>;
}