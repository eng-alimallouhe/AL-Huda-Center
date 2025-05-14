using LMS.Domain.Entities.Users;

namespace LMS.Domain.Entities.HR
{
    public class Attendance
    {
        //primary key
        public Guid AttendanceId { get; set; }

        
        //Foreign Key: EmployeeId ==> one(employee)-to-many(attendances) relationship
        public Guid EmployeeId { get; set; }

        public DateTime Date { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public bool? IsHoliday { get; set; }

        //Soft Delete

        public bool IsActive { get; set; }

        
        //Navigation Property:
        public Employee Employee { get; set; }

        public Attendance()
        {
            AttendanceId = Guid.NewGuid();
            IsActive = true;
            Employee = null!;
        }
    }
}
