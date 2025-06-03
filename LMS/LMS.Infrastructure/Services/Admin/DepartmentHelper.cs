using AutoMapper;
using LMS.Application.Abstractions.Services.Admin;
using LMS.Application.DTOs.Admin.Departments;
using LMS.Application.DTOs.Admin.Employees;
using LMS.Application.DTOs.Orders;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Orders;
using LMS.Domain.Entities.Users;

namespace LMS.Infrastructure.Services.Admin
{
    public class DepartmentHelper : IDepartmentHelper
    {
        private readonly IMapper _mapper;
        private readonly ISoftDeletableRepository<BaseOrder> _baseOrderRepo;
        private readonly ISoftDeletableRepository<EmployeeDepartment> _employeeDepartmentRepo;

        public DepartmentHelper(
            IMapper mapper,
            ISoftDeletableRepository<BaseOrder> baseOrderRepo,
            ISoftDeletableRepository<EmployeeDepartment> employeeDepartmentRepo)
        {
            _mapper = mapper;
            _baseOrderRepo = baseOrderRepo;
            _employeeDepartmentRepo = employeeDepartmentRepo;
        }


        public async Task<DepartmentDetailsDTO> BuildDepartmentResponseAsync(Department department)
        {
            var departmentDetails = _mapper.Map<DepartmentDetailsDTO>(department);

            var empDeps = await _employeeDepartmentRepo.GetAllAsync(new Specification<EmployeeDepartment>(
                criteria: empDeps => empDeps.DepartmentId == department.DepartmentId,
                includes: ["Employee.EmployeeDepartments.Department"]
                ));


            var currentEmployeeDeps = empDeps.items.Where(ed => ed.IsActive).ToList();
            var formerEmployeeDeps = empDeps.items.Where(ed => !ed.IsActive).ToList();


            var currentEmployees = currentEmployeeDeps.Select(ed => ed.Employee).Distinct().ToList();
            var formerEmployees = formerEmployeeDeps.Select(ed => ed.Employee).Distinct().ToList();



            departmentDetails.CurrentEmployees = _mapper.Map<ICollection<EmployeeOverviewDto>>(currentEmployees);
            departmentDetails.FormerEmployees = _mapper.Map<ICollection<EmployeeOverviewDto>>(formerEmployees);



            var orders = await _baseOrderRepo.GetAllAsync(new Specification<BaseOrder>(
                criteria: baseOrder => baseOrder.DepartmentId == department.DepartmentId
                ));

            departmentDetails.CurrentOrders = _mapper.Map<ICollection<OrderOverviewDto>>(orders.items);

            return departmentDetails;
        }
    
    }
}