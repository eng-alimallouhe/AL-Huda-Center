using AutoMapper;
using LMS.Application.DTOs.Admin.Departments;
using LMS.Application.DTOs.Common;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Users;
using MediatR;

namespace LMS.Application.Features.Admin.Departments.Queries.GetAllDepartments
{
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, PagedResult<DepartmentOverviewDto>>
    {
        private readonly ISoftDeletableRepository<Department> _departmentRepo;
        private readonly IMapper _mapper;

        public GetAllDepartmentsQueryHandler(
            ISoftDeletableRepository<Department> departmentRepo,
            IMapper mapper)
        {
            _departmentRepo = departmentRepo;
            _mapper = mapper;
        }

        public async Task<PagedResult<DepartmentOverviewDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var response = await _departmentRepo.GetAllAsync(new Specification<Department>(
                criteria: department => department.IsActive == true,
                take: request.PageSize,
                skip: request.PageNumber,
                includes: ["EmployeeDepartments.Employee"]
                ));


            var departments =  _mapper.Map<ICollection<DepartmentOverviewDto>>(response.items);


            return new PagedResult<DepartmentOverviewDto> 
            { 
                Items = departments,
                PageSize = request.PageSize,
                CurrentPage = request.PageNumber,
                TotalCount = response.count,
            };
        }
    }
}
