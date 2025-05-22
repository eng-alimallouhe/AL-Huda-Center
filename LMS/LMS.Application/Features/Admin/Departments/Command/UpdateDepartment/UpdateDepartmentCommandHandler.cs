using AutoMapper;
using LMS.Common.Enums;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Departments.Command.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Result>
    {
        private readonly ISoftDeletableRepository<Department> _departmentRepo;
        private readonly IMapper _mapper;

        public UpdateDepartmentCommandHandler(
            ISoftDeletableRepository<Department> departmentRepo,
            IMapper mapper)
        {
            _departmentRepo = departmentRepo;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepo.GetByIdAsync(request.DepartmentId);

            if (department is null)
            {
                return Result.Failure(ResponseStatus.DEPARTMENT_NOT_FOUNDED);
            }

            _mapper.Map(request, department);

            await _departmentRepo.UpdateAsync(department);

            return Result.Success(ResponseStatus.TASK_COMPLETED);
        }
    }
}
