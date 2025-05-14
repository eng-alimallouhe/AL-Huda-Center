using LMS.Common.Exceptions;
using LMS.Domain.Entities.Users;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Users
{

    public class AddressRepository : SoftDeletableRepository<Address>
    {
        private readonly LMSDbContext _context;
        public AddressRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address != null)
            {
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException("Address not found");
            }
        }
    }
}
