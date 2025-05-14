using LMS.Common.Exceptions;
using LMS.Domain.Entities.Users;
using LMS.Infrastructure.DbContexts;

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
                await _context.SaveChangesAsync();
            }

            else
            {
                throw new EntityNotFoundException("EmployeeDepartment not found");
            }
        }


    }
}
