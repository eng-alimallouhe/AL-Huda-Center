using AutoMapper;
using LMS.Common.Enums;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Users;
using MediatR;
using Microsoft.IdentityModel.Abstractions;

namespace LMS.Application.Features.Admin.Employees.Command.TransferEmployee
{
    public class TransferEmployeeCommandHandler : IRequestHandler<TransferEmployeeCommand, Result>
    {
        private readonly ISoftDeletableRepository<Employee> _employeeRepo;
        private readonly ISoftDeletableRepository<Department> _departmentRepo;
        private readonly ISoftDeletableRepository<EmployeeDepartment> _empDepRepo;
        private readonly IMapper _mapper;

        public TransferEmployeeCommandHandler(
            ISoftDeletableRepository<Employee> employeeRepo,
            ISoftDeletableRepository<Department> departmentRepo,
            ISoftDeletableRepository<EmployeeDepartment> empDepRepo,
            IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _departmentRepo = departmentRepo;
            _empDepRepo = empDepRepo;
            _mapper = mapper;
        }

        public async Task<Result> Handle(TransferEmployeeCommand request, CancellationToken cancellationToken)
        {
            var departemtn = await _departmentRepo.GetByIdAsync(request.DepartmentId);

            if (departemtn is null)
            {
                return Result.Failure(ResponseStatus.DEPARTMENT_NOT_FOUNDED);
            }

            var employee = await _employeeRepo.GetByIdAsync(request.EmployeeId);

            if(employee is null)
            {
                return Result.Failure(ResponseStatus.ACCOUNT_NOT_FOUND);
            }

            var oldEmpDep = await _empDepRepo.GetBySpecificationAsync(new Specification<EmployeeDepartment>(
                criteria: empDep => empDep.IsActive == true &&
                        empDep.EmployeeId == request.EmployeeId 
                ));

            if (oldEmpDep is not null)
            {
                if (oldEmpDep.DepartmentId == request.DepartmentId)
                {
                    return Result.Failure(ResponseStatus.EXISTING_APPOINTMENT);
                }

                oldEmpDep.IsActive = false;
                await _empDepRepo.UpdateAsync(oldEmpDep);
            }

            var employeeDep = _mapper.Map<EmployeeDepartment>(request);

            await _empDepRepo.AddAsync(employeeDep);

            return Result.Success(ResponseStatus.TASK_COMPLETED);
        }
    }
}
