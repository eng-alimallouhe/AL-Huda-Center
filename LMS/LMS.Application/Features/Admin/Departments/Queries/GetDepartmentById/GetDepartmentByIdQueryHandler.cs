using LMS.Application.Abstractions.Services.Admin;
using LMS.Application.DTOs.Admin.Departments;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Departments.Queries.GetDepartmentById
{
    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDetailsDTO?>
    {
        private readonly ISoftDeletableRepository<Department> _departmentRepo;
        private readonly IDepartmentHelper _departmentHelper;

        public GetDepartmentByIdQueryHandler(
            ISoftDeletableRepository<Department> departmentRepo,
            IDepartmentHelper departmentHelper)
        {
            _departmentRepo = departmentRepo;
            _departmentHelper = departmentHelper;
        }

        public async Task<DepartmentDetailsDTO?> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepo.GetBySpecificationAsync(new Specification<Department>(
                criteria: department => department.DepartmentId == request.Id
                ));

            if (department is null)
            {
                return null;
            }

            return await _departmentHelper.BuildDepartmentResponseAsync(department);
        }
    }
}
