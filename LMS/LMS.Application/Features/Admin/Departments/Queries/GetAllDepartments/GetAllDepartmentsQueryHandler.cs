using AutoMapper;
using LMS.Application.DTOs.Admin.Departments;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Departments.Queries.GetAllDepartments
{
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, ICollection<DepartmentOverviewDto>>
    {
        private readonly ISoftDeletableRepository<Department> _departmentRepo;
        private readonly IMapper _mapper;

        public GetAllDepartmentsQueryHandler(
            ISoftDeletableRepository<Department> departmentRepo,
            IMapper mapper)
        {
            _departmentRepo = departmentRepo;
            _mapper = mapper;
        }

        public async Task<ICollection<DepartmentOverviewDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var deps = await _departmentRepo.GetAllAsync(new Specification<Department>(
                criteria: department => department.IsActive == true,
                includes: ["EmployeeDepartments.Employee"]
                ));

            return _mapper.Map<ICollection<DepartmentOverviewDto>>(deps);
        }
    }
}
