using LMS.Application.Abstractions.Services.Admin;
using LMS.Application.DTOs.Admin.Employees;
using LMS.Common.Enums;
using LMS.Common.Results;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;

namespace LMS.Infrastructure.Services.Admin
{
    public class EmployeeHelper : IEmployeeHelper
    {
        private readonly ISoftDeletableRepository<EmployeeDepartment> _empDepRepo;
        private readonly ISoftDeletableRepository<Employee> _employeeRepo;
        private readonly ISoftDeletableRepository<Department> _departmenRepo;
        private readonly ISoftDeletableRepository<User> _userRepo;
        private readonly ISoftDeletableRepository<Role> _roleRepo;

        public EmployeeHelper(
            ISoftDeletableRepository<EmployeeDepartment> empDepRepo,
            ISoftDeletableRepository<Employee> employeeRepo,
            ISoftDeletableRepository<Department> departmentRepo,
            ISoftDeletableRepository<User> userRepo,
            ISoftDeletableRepository<Role> roleRepo)
        {
            _empDepRepo = empDepRepo;
            _departmenRepo = departmentRepo;
            _employeeRepo = employeeRepo;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
        }

        public async Task<Result<EmployeeCreatignResultDto>> CreateEmployee(Employee employee, Guid departmenId)
        {
            var department = await _departmenRepo.GetByIdAsync(departmenId);

            if (department is null)
            {
                return Result<EmployeeCreatignResultDto>.Failure(ResponseStatus.DEPARTMENT_NOT_FOUNDED);
            }

            employee.IsEmailConfirmed = true;

            var password = GenerateRandomPassword(13);

            employee.HashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            employee.UserName = await GenerateRandomUserName(employee.FullName);

            var employeesRole = await _roleRepo.GetByExpressionAsync(role => 
                role.RoleType.ToLower() == "employee");

            if (employeesRole is null)
            {
                return Result<EmployeeCreatignResultDto>.Failure(ResponseStatus.SOURCE_NOT_FOUND);
            }

            employee.RoleId = employeesRole.RoleId;
            employee.Role = employeesRole;

            await _employeeRepo.AddAsync(employee);

            var employeeDep = new EmployeeDepartment
            {
                AppointmentDecisionUrl = string.Empty,
                EmployeeId = employee.UserId,
                DepartmentId = department.DepartmentId,
                StartDate = DateTime.UtcNow,
            };

            await _empDepRepo.AddAsync(employeeDep);

            return Result<EmployeeCreatignResultDto>.Success(new EmployeeCreatignResultDto
            {
                Email = employee.Email,
                UserName = employee.UserName,
                Password = password
            }, ResponseStatus.TASK_COMPLETED);
        }

        private async Task<string> GenerateRandomUserName(string cleanName)
        {
            cleanName = cleanName.Replace(" ", "_");

            var random = new Random();

            string finalResult = cleanName;

            bool isUnique = false;

            while (!isUnique)
            {
                finalResult = cleanName;

                for (int i = 0; i < 4; i++)
                {
                    var randomNuber = random.Next(65, 91);

                    var randomChar = (char)randomNuber;

                    finalResult += randomChar;
                }

                finalResult += random.Next(10, 99);

                var user = await _userRepo.GetByExpressionAsync(user => 
                user.UserName == finalResult);

                if (user is null)
                {
                    isUnique = true;
                }
            }

            return finalResult;
        }

        private string GenerateRandomPassword(int passwordLength)
        {
            var finalResult = new char[passwordLength];

            var random = new Random();  

            var chars = new List<char>();

            chars.AddRange("abcdefghijklmnopqrstuvwxyz");
            chars.AddRange("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            chars.AddRange("0123456789");
            chars.AddRange("!@#$%^&*_");

            for (int i = 0; i < passwordLength; i++)
            {
                finalResult[i] = chars[random.Next(chars.Count)];
            }

            return new string(finalResult);
        }
    }
}