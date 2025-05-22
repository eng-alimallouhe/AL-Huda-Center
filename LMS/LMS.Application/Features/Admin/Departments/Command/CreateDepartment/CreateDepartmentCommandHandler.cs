using AutoMapper;
using LMS.Common.Enums;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Departments.Command.CreateDepartment
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result>
    {
        private readonly ISoftDeletableRepository<Department> _departmentRepo;
        private readonly IMapper _mapper;

        public CreateDepartmentCommandHandler(
            ISoftDeletableRepository<Department> departmentRepo,
            IMapper mapper)
        {
            _departmentRepo = departmentRepo;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var newDepartment = _mapper.Map<Department>(request);

            await _departmentRepo.AddAsync(newDepartment);

            return Result.Success(ResponseStatus.TASK_COMPLETED);
        }
    }
}
