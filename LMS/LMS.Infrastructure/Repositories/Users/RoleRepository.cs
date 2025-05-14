using LMS.Common.Exceptions;
using LMS.Domain.Entities.Users;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Users
{
    public class RoleRepository : SoftDeletableRepository<Role>
    {
        private readonly LMSDbContext _context;
        public RoleRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var role = await _context.Roles.FindAsync(id);
            
            if (role != null)
            {
                role.IsActive = false;
                _context.Roles.Update(role);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException("Role not found");
            }
        }
    }
}
