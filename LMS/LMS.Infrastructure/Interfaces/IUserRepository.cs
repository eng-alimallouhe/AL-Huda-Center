using LMS.Domain.Entities.Users;
using LMS.Domain.Interfaces;

namespace LMS.Infrastructure.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task LockUser(Guid userId);
    }
}
