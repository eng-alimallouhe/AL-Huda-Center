using LMS.Domain.Entites.Users;
using LMS.Domain.Enums.HR;

namespace LMS.Domain.Entites.HR
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
        public decimal? WorkDuration { get; set; }
        public AttendanceStatus AttendanceStatus { get; set; }
        public bool? IsHoliday { get; set; }
        public string? LateArrivalReason { get; set; }

        
        public Attendance()
        {
            AttendanceId = Guid.NewGuid();
        }
    }
}
