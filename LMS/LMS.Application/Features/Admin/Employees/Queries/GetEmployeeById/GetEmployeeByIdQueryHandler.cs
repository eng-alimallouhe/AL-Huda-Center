using AutoMapper;
using LMS.Application.DTOs.Admin.Employees;
using LMS.Application.DTOs.Admin.HR;
using LMS.Application.Specifications.Users;
using LMS.Domain.Abstractions.Repositories;
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
            var employee = await _employeeRepo.GetBySpecificationAsync(new SingleEmployeeSpecification(request.Id));

            if (employee is null)
            {
                return null;
            }

            var result = _mapper.Map<EmployeeDetailsDto>(employee);


            result.AttendancesView = _mapper.Map<ICollection<AttendanceOverviewDto>>(
                employee.Attendances
                .Where(a => a.IsActive && a.Date.Month == DateTime.Now.Month),
                opt =>
                {
                    opt.Items["Language"] = request.Language.ToString().ToLower();
                });
            

            result.IncentivesView = _mapper.Map<ICollection<IncentivesOverViewDto>>(
                employee.Incentives.Where(a => a.IsActive));
            

            result.PenaltiesView = _mapper.Map<ICollection<PenaltiesOverviewDto>>(
                employee.Penalties.Where(a => a.IsActive));
            

            result.LeavesView = _mapper.Map<ICollection<LeavesOverViewDto>>(
                employee.Leaves.Where(a => a.IsActive));


            result.SalariesView = _mapper.Map<ICollection<SalaiesOverviewDto>>(
                employee.Salaries.Where(s => s.IsActive));


            return result;
        }
    }
}
