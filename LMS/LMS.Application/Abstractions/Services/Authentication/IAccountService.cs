using LMS.Common.Results;
using LMS.Domain.Entities.Users;

namespace LMS.Application.Abstractions.Services.Authentication
{
    public interface IAccountService
    {
        Task<Result> LogIn(User user, string email, string password);
    }
}
