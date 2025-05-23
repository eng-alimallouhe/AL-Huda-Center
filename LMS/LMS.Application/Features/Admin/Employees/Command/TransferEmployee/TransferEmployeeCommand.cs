using LMS.Common.Results;
using MediatR;

namespace LMS.Application.Features.Admin.Employees.Command.TransferEmployee
{
    public record TransferEmployeeCommand(
        Guid EmployeeId,
        Guid DepartmentId,
        string? AppointmentDecisionUrl = " ") : IRequest<Result>;
}
