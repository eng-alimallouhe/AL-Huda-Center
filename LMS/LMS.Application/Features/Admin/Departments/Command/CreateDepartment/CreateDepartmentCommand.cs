using LMS.Common.Results;
using LMS.Domain.Enums.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Departments.Command.CreateDepartment
{
    public record CreateDepartmentCommand(
        string DepartmentName, 
        string DepartmentDescription,
        ResponsibilityType ResponsibilityType) : IRequest<Result>;
}
