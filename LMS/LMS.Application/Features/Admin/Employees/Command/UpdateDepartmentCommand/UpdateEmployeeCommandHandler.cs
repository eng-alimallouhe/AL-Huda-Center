using AutoMapper;
using LMS.Common.Enums;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Employees.Command.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Result>
    {
        private readonly ISoftDeletableRepository<Employee> _employeeRepo;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(
            ISoftDeletableRepository<Employee> employeeRepo,
            IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepo.GetByIdAsync(request.EmployeeId);

            var anotherEmployee = await _employeeRepo.GetByExpressionAsync(user => 
                user.Email.ToLower().Trim() == request.Email.ToLower().Trim());

            if (employee is null)
            {
                return Result.Failure(ResponseStatus.ACCOUNT_NOT_FOUND);
            }

            if (anotherEmployee is not null && anotherEmployee.UserId != employee.UserId)
            {
                return Result.Failure(ResponseStatus.EXISTING_ACCOUNT);
            }

            _mapper.Map(request, employee);

            employee.UpdatedAt = DateTime.UtcNow;

            await _employeeRepo.UpdateAsync(employee);

            return Result.Success(ResponseStatus.TASK_COMPLETED);
        }
    }
}