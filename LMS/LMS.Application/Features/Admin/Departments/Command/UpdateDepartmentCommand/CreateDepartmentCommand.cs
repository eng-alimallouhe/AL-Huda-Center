using LMS.Common.Results;
using MediatR;

namespace LMS.Application.Features.Admin.Departments.Command.CreateDepartment
{
    public record UpdateDepartmentCommand(Guid DepartmentId, string DepartmentName, string DepartmentDescription) : IRequest<Result>;
}
