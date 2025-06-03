using LMS.Domain.Entities.HR;
using LMS.Domain.Enums.HR;

namespace LMS.Application.DTOs.Admin.HR
{
    public class LeavesOverViewDto
    {
        public Guid LeaveId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string LeaveType { get; set; }
        public string Reason { get; set; }
        public string IsAbroved { get; set; }
    }
}
