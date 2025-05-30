using AutoMapper;
using LMS.Application.DTOs.Admin.Employees;
using LMS.Application.DTOs.Common;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Employees.Queries.GetAllGetAllEmployees
{
    public class GetAllGetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, PagedResult<EmployeeOverviewDto>>
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

        public async Task<PagedResult<EmployeeOverviewDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var response = await _employeeRepo.GetAllAsync(new Specification<Employee>(
                criteria: employee => employee.IsDeleted != true,
                includes: ["EmployeeDepartments.Department"]
                ));

            var employees =  _mapper.Map<ICollection<EmployeeOverviewDto>>(response.items);

            return new PagedResult<EmployeeOverviewDto>(employees, response.count, request.PageSize, request.PageNumber);
        }
    }
}