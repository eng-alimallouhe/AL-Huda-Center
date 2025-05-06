using LMS.Domain.Entities.Users;
using LMS.Infrastructure.DbContexts;
using LMS.Infrastructure.Repositories.Base;

namespace LMS.Infrastructure.Repositories.Users
{
    public class EmployeeDepartmentRepository : SoftDeletableRepository<EmployeeDepartment>
    {
        private readonly LMSDbContext _context;
        public EmployeeDepartmentRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var employeeDepartment = await _context.EmployeeDepartments.FindAsync(id);
            if (employeeDepartment != null)
            {
                employeeDepartment.IsActive = false;
                _context.EmployeeDepartments.Update(employeeDepartment);
                await SaveChangesAsync();
            }

            else
            {
                throw new Exception("EmployeeDepartment not found");
            }
        }


    }
}
