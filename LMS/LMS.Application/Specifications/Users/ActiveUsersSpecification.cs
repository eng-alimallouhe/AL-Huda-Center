using System.Linq.Expressions;
using LMS.Domain.Abstractions.Repositories;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.Users;

namespace LMS.Application.Specifications.Users
{
    public class ActiveUsersSpecification : ISpecification<User>
    {
        public ActiveUsersSpecification()
        {

        }

        public Expression<Func<User, bool>>? Criteria => 
            user => user.IsDeleted == false  && user.IsLocked == false;

        public List<string> Includes => ["Role"];

        public bool IsTrackingEnabled => false;

        public Expression<Func<User, object>>? OrderBy => null;

        public Expression<Func<User, object>>? OrderByDescending => null;

        public int? Skip => null;

        public int? Take => null;
    }
}
