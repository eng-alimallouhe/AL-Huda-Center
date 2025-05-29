using AutoMapper;
using LMS.Application.Abstractions.Services.Admin;
using LMS.Application.DTOs.Admin.Departments;
using LMS.Application.DTOs.Admin.Employees;
using LMS.Application.DTOs.Orders;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Users;

namespace LMS.Infrastructure.Services.Admin
{
    public class DepartmentHelper : IDepartmentHelper
    {
        private readonly IMapper _mapper;
        private readonly ISoftDeletableRepository<Employee> _employeeRepo;
        private readonly ISoftDeletableRepository<EmployeeDepartment> _employeeDepartmentRepo;

        public DepartmentHelper(
            IMapper mapper,
            ISoftDeletableRepository<Employee> employeeRepo,
            ISoftDeletableRepository<EmployeeDepartment> employeeDepartmentRepo)
        {
            _mapper = mapper;
            _employeeRepo = employeeRepo;
            _employeeDepartmentRepo = employeeDepartmentRepo;
        }

        public async Task<DepartmentDetailsDTO> BuildDepartmentResponseAsync(Department department)
        {
            var departmentDetails = _mapper.Map<DepartmentDetailsDTO>(department);

            var empDeps = await _employeeDepartmentRepo.GetAllAsync(new Specification<EmployeeDepartment>(
                criteria: empDeps => empDeps.DepartmentId == department.DepartmentId,
                includes: ["Employee"]
                ));

            var activeEmployees = empDeps.Where(empDeps => empDeps.IsActive).Select(empDeps => empDeps.Employee);
            var inActiveEmployees = empDeps.Where(empDeps => !empDeps.IsActive).Select(empDeps => empDeps.Employee);

            departmentDetails.CurrentEmployees = _mapper.Map<ICollection<EmployeeOverviewDto>>(activeEmployees);
            departmentDetails.FormerEmployees = _mapper.Map<ICollection<EmployeeOverviewDto>>(inActiveEmployees);

            return departmentDetails;
        }
    
    }
}