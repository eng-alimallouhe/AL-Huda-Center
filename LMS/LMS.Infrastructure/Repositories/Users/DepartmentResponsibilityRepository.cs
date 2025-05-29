using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Entities.Users;
using LMS.Infrastructure.DbContexts;

namespace LMS.Infrastructure.Repositories.Users
{
    public class DepartmentResponsibilityRepository : 
        BaseRepository<DepartmentResponsibility>
    {
        public DepartmentResponsibilityRepository(LMSDbContext context) : base(context) { }
    }
}
