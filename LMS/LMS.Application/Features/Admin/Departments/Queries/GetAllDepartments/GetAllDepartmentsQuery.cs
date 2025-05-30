using LMS.Application.DTOs.Admin.Departments;
using LMS.Application.DTOs.Common;
using MediatR;

namespace LMS.Application.Features.Admin.Departments.Queries.GetAllDepartments
{
    public record GetAllDepartmentsQuery(
        int PageSize = 10,
        int PageNumber = 0) : IRequest<PagedResult<DepartmentOverviewDto>>;
}
