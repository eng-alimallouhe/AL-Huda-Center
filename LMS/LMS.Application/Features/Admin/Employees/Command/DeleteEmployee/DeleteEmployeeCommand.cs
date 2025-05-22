using LMS.Common.Results;
using MediatR;

namespace LMS.Application.Features.Admin.Employees.Command.DeleteEmployee
{
    public record DeleteEmployeeCommand(Guid DepartmentId) : IRequest<Result>;
}
