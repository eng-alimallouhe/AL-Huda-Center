using System.Linq.Expressions;
using LMS.Domain.Abstractions.Specifications;
using LMS.Domain.Entities.HR;
using LMS.Domain.Entities.Users;

namespace LMS.Application.Specifications.Users
{
    public class SingleEmployeeSpecification : ISpecification<Employee>
    {
        private readonly Guid _id;

        public Expression<Func<Employee, bool>>? Criteria => 
                employee => employee.UserId == _id;

        public List<string> Includes => [
            "FinancialRevenues",
            "Attendances",
            "Incentives",
            "Penalties",
            "Leaves",
            "LeaveBalance",
            "Orders",
            "EmployeeDepartments.Department",

            ];

        public bool IsTrackingEnabled => false;

        public Expression<Func<Employee, object>>? OrderBy => null;

        public Expression<Func<Employee, object>>? OrderByDescending => null;

        public int? Skip => null;

        public int? Take => null;

        public SingleEmployeeSpecification(Guid id)
        {
            _id = id;
        }
    }
}
