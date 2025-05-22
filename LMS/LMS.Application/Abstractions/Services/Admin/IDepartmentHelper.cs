using LMS.Application.DTOs.Admin.Departments;
using LMS.Domain.Entities.Users;

namespace LMS.Application.Abstractions.Services.Admin
{
    public interface IDepartmentHelper
    {
        Task<DepartmentDetailsDTO> BuildDepartmentResponseAsync(Department department);
    }
}
