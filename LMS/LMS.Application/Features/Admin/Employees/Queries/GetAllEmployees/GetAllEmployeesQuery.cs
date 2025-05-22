using LMS.Application.DTOs.Admin.Departments;
using LMS.Application.DTOs.Admin.Employees;
using MediatR;

namespace LMS.Application.Features.Admin.Employees.Queries.GetAllGetAllEmployees
{
    public record GetAllEmployeesQuery() : IRequest<ICollection<EmployeeOverviewDto>>;
}
