using LMS.Domain.Entities.Users;
using LMS.Infrastructure.DbContexts;
using LMS.Infrastructure.Repositories.Base;

namespace LMS.Infrastructure.Repositories.Users
{
    public class DepartmentRepository : SoftDeletableRepository<Department>
    {
        private readonly LMSDbContext _context;
        public DepartmentRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                department.IsActive = false;
                _context.Departments.Update(department);
                await SaveChangesAsync();
            }
            else
            {
                throw new Exception("Department not found");
            }
        }


    }
}
