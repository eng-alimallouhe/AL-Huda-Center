using AutoMapper;
using LMS.Application.DTOs.Admin.Employees;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Employees.Queries.GetAllGetAllEmployees
{
    public class GetAllGetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, ICollection<EmployeeOverviewDto>>
    {
        private readonly ISoftDeletableRepository<Employee> _employeeRepo;
        private readonly IMapper _mapper;

        public GetAllGetAllEmployeesQueryHandler(
            ISoftDeletableRepository<Employee> employeeRepo,
            IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }

        public async Task<ICollection<EmployeeOverviewDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepo.GetAllAsync(new Specification<Employee>(
                criteria: employee => employee.IsDeleted != true,
                includes: [employee => employee.EmployeeDepartments.Where(ed => ed.IsActive).Select(ed => ed.Department)]
                ));

            return _mapper.Map<ICollection<EmployeeOverviewDto>>(employees);
        }
    }
}
