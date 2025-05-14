using LMS.Domain.Entities.Users;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Users
{
    public class CustomerRepository : SoftDeletableRepository<Customer>
    {
        private readonly LMSDbContext _context;

        public CustomerRepository(
            LMSDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override async Task SoftDeleteAsync(Guid id)
        {
            var user = await _context.Customers.FindAsync(id);

            if (user == null)
            {
                throw new EntryPointNotFoundException("Not found");
            }

            user.IsDeleted = true;
            _context.Customers.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
