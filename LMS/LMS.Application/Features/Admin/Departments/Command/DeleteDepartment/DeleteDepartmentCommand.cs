using LMS.Common.Results;
using MediatR;

namespace LMS.Application.Features.Admin.Departments.Command.DeleteDepartment
{
    public record DeleteDepartmentCommand(Guid DepartmentId) : IRequest<Result>;
}
