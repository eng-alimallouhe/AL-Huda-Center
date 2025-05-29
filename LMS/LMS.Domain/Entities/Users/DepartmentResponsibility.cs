using LMS.Domain.Enums.Users;

namespace LMS.Domain.Entities.Users
{
    public class DepartmentResponsibility
    {
        public Guid DepartmentResponsibilityId { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } 

        public ResponsibilityType ResponsibilityType { get; set; }

        public DepartmentResponsibility()
        {
            DepartmentResponsibilityId = Guid.NewGuid();
            Department = null!;
        }
    }
}
