using LMS.Application.DTOs.Admin.Employees;
using LMS.Common.Results;
using LMS.Domain.Enums.Commons;
using MediatR;

namespace LMS.Application.Features.Admin.Employees.Command.CreateDepartment
{
    public record CreateEmployeeCommand(
        string FullName,
        string Email,
        string Phonenumber,
        Language Language,
        Guid DepartmentId,
        string? ProfilePictureUrl = " ") : IRequest<Result<EmployeeCreatignResultDto>>;
}
