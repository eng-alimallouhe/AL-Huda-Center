using LMS.Common.Results;
using LMS.Domain.Enums.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Departments.Command.UpdateDepartment
{
    public record UpdateDepartmentCommand(
        string DepartmentName, 
        string DepartmentDescription,
        Guid? DepartmentId, 
        ResponsibilityType ResponsibilityType) : IRequest<Result>;
}
