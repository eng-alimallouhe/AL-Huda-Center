using LMS.Domain.Entities.Users;
using LMS.Domain.Interfaces;
using LMS.Infrastructure.DbContexts;
using LMS.Infrastructure.Repositories.Base;

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
                await SaveChangesAsync();
            }
            
            else
            {
                throw new Exception("Role not found");
            }
        }
    }
}
