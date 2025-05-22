using LMS.Application.DTOs.Admin.Departments;
using MediatR;

namespace LMS.Application.Features.Admin.Departments.Queries.GetDepartmentById
{
    public record GetDepartmentByIdQuery(Guid Id) : IRequest<DepartmentDetailsDTO?>;
}
