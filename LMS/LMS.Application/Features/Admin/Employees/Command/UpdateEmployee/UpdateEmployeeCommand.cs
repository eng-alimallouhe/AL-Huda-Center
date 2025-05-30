using LMS.Common.Results;
using MediatR;

namespace LMS.Application.Features.Admin.Employees.Command.UpdateEmployee
{
    public record UpdateEmployeeCommand(
        Guid EmployeeId, 
        string FullName,
        string Email,
        decimal BaseSalary,
        string PhoneNumber) : IRequest<Result>;
}