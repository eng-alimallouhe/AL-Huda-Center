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
        private readonly IBaseRepository<DepartmentResponsibility> _depRespRepo;
        private readonly IMapper _mapper;

        public CreateDepartmentCommandHandler(
            ISoftDeletableRepository<Department> departmentRepo,
            IBaseRepository<DepartmentResponsibility> depRespRepo,
            IMapper mapper)
        {
            _departmentRepo = departmentRepo;
            _depRespRepo = depRespRepo;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var isExsitingRespon = await _depRespRepo.GetByExpressionAsync(depResp => depResp.ResponsibilityType == request.ResponsibilityType) is not null;

            if (isExsitingRespon)
            {
                return Result.Failure(ResponseStatus.USED_REPOSNSIBILITY);
            }
          
            var newDepartment = _mapper.Map<Department>(request);

            var newResponsibilty = new DepartmentResponsibility
            {
                ResponsibilityType = request.ResponsibilityType,
                DepartmentId = newDepartment.DepartmentId
            };

            await _departmentRepo.AddAsync(newDepartment);

            await _depRespRepo.AddAsync(newResponsibilty);

            return Result.Success(ResponseStatus.TASK_COMPLETED);
        }
    }
}
