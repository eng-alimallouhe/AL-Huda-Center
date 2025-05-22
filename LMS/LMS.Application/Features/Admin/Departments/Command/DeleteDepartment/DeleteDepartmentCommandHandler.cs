using LMS.Common.Enums;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Departments.Command.DeleteDepartment
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Result>
    {

        private readonly ISoftDeletableRepository<Department> _derpartmentRepo;

        public DeleteDepartmentCommandHandler(
            ISoftDeletableRepository<Department> derpartmentRepo)
        {
            _derpartmentRepo = derpartmentRepo;
        }

        public async Task<Result> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            await _derpartmentRepo.SoftDeleteAsync(request.DepartmentId);

            return Result.Success(ResponseStatus.TASK_COMPLETED);
        }
    }
}