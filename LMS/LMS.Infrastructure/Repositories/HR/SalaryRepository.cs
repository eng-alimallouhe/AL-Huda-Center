
using LMS.Domain.Entities.HR;
using LMS.Infrastructure.DbContexts;
using LMS.Infrastructure.Repositories.Base;

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
                await SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Salary not found");
            }
        }
    }

}
