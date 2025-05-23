using AutoMapper;
using LMS.Application.DTOs.Admin.Employees;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDetailsDto?>
    {
        private readonly ISoftDeletableRepository<Employee> _employeeRepo;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(
            ISoftDeletableRepository<Employee> employeeRepo,
            IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }

        public async Task<EmployeeDetailsDto?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepo.GetBySpecificationAsync(new Specification<Employee>(
                criteria: employee => employee.UserId == request.Id,
                includes: [
                    "FinancialRevenues",
                    "EmployeeDepartments.Department"
                    ],
                tracking: false
                ));

            if (employee is null)
            {
                return null;
            }

            return _mapper.Map<EmployeeDetailsDto>(employee);
        }
    }
}
