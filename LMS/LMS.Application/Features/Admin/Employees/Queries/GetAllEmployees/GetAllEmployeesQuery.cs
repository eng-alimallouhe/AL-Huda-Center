using LMS.Application.DTOs.Admin.Employees;
using LMS.Application.DTOs.Common;
using MediatR;

namespace LMS.Application.Features.Admin.Employees.Queries.GetAllGetAllEmployees
{
    public record GetAllEmployeesQuery(
        int PageSize = 0, 
        int PageNumber = 0) : IRequest<PagedResult<EmployeeOverviewDto>>;
}