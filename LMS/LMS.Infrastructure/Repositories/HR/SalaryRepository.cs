
using LMS.Common.Exceptions;
using LMS.Domain.Entities.HR;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.HR
{
    public class SalaryRepository : SoftDeletableRepository<Salary>
    {
        private readonly LMSDbContext _context;
        public SalaryRepository(LMSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task SoftDeleteAsync(Guid id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            if (salary is not null)
            {
                salary.IsActive = false;
                _context.Salaries.Update(salary);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException("Salary not found");
            }
        }
    }

}
