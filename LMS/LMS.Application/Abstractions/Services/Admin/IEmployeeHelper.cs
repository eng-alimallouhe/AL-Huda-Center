using LMS.Application.DTOs.Admin.Employees;
using LMS.Common.Results;
using LMS.Domain.Entities.Users;

namespace LMS.Application.Abstractions.Services.Admin
{
    public interface IEmployeeHelper
    {
        Task<Result<EmployeeCreatignResultDto>> CreateEmployee(Employee employee, Guid departmenId);
    }
}
