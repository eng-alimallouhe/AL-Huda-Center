using LMS.Common.Enums;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Employees.Command.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Result>
    {

        private readonly ISoftDeletableRepository<Employee> _employeeRepo;

        public DeleteEmployeeCommandHandler(
            ISoftDeletableRepository<Employee> employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeRepo.SoftDeleteAsync(request.DepartmentId);

            return Result.Success(ResponseStatus.TASK_COMPLETED);
        }
    }
}