using AutoMapper;
using LMS.Common.Enums;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Departments.Command.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Result>
    {
        private readonly ISoftDeletableRepository<Department> _departmentRepo;
        private readonly IBaseRepository<DepartmentResponsibility> _depRespRepo;
        private readonly IMapper _mapper;

        public UpdateDepartmentCommandHandler(
            ISoftDeletableRepository<Department> departmentRepo,
            IBaseRepository<DepartmentResponsibility> depRespRepo,
            IMapper mapper)
        {
            _departmentRepo = departmentRepo;
            _depRespRepo = depRespRepo;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepo.GetBySpecificationAsync(new Specification<Department>(
                criteria: department => department.DepartmentId == request.DepartmentId,
                includes: ["Responsibility"]
                ));

            if (department is null)
            {
                return Result.Failure(ResponseStatus.DEPARTMENT_NOT_FOUNDED);
            }

            var departmentReponsibility = department.Responsibility;

            if (departmentReponsibility is null)
            {
                return Result.Failure(ResponseStatus.DEPARTMENT_NOT_FOUNDED);
            }

            if (departmentReponsibility.ResponsibilityType == request.ResponsibilityType)
            {
                return Result.Failure(ResponseStatus.SAME_DETAIL);
            }

            var exsitingResponsibilty = await _depRespRepo.GetByExpressionAsync(
                responsibility => responsibility.ResponsibilityType == request.ResponsibilityType && 
                    responsibility.DepartmentId != request.DepartmentId);


            if (exsitingResponsibilty is not null)
            {
                return Result.Failure(ResponseStatus.USED_REPOSNSIBILITY);
            }

            if (departmentReponsibility.ResponsibilityType != request.ResponsibilityType)
            {
                departmentReponsibility.ResponsibilityType = request.ResponsibilityType;
                await _depRespRepo.UpdateAsync(departmentReponsibility);
            }

            if (! request.DepartmentName.Equals(department.DepartmentName, StringComparison.OrdinalIgnoreCase) || 
                ! request.DepartmentDescription.Equals(department.DepartmentDescription, StringComparison.OrdinalIgnoreCase))
            {
                _mapper.Map(request, department);
                await _departmentRepo.UpdateAsync(department);
            }
            return Result.Success(ResponseStatus.TASK_COMPLETED);
        }
    }
}