using AutoMapper;
using LMS.Application.Abstractions.Services.Admin;
using LMS.Application.DTOs.Admin.Employees;
using LMS.Common.Enums;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Employees.Command.CreateDepartment
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<EmployeeCreatignResultDto>>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeHelper _employeeHelper;
        private readonly ISoftDeletableRepository<User> _userRepo;

        public CreateEmployeeCommandHandler(
            IEmployeeHelper employeeHelper,
            ISoftDeletableRepository<User> userRepo,
            IMapper mapper)
        {
            _employeeHelper = employeeHelper;
            _mapper = mapper;
            _userRepo = userRepo;
        }

        async Task<Result<EmployeeCreatignResultDto>> IRequestHandler<CreateEmployeeCommand, Result<EmployeeCreatignResultDto>>.Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepo.GetByExpressionAsync(
                user => user.Email.ToLower().Trim() == request.Email.ToLower().Trim()
                );

            if (existingUser is not null)
            {
                return Result<EmployeeCreatignResultDto>.Failure(ResponseStatus.EXISTING_ACCOUNT);
            }

            var employee = _mapper.Map<Employee>(request);

            var result = await _employeeHelper.CreateEmployee(employee, request.DepartmentId);

            return result;
        }
    }
}
