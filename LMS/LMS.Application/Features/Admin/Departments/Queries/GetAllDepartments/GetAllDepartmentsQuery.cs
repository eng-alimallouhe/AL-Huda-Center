using LMS.Application.DTOs.Admin.Departments;
using MediatR;

namespace LMS.Application.Features.Admin.Departments.Queries.GetAllDepartments
{
    public record GetAllDepartmentsQuery() : IRequest<ICollection<DepartmentOverviewDto>>;
}
