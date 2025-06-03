namespace LMS.Application.DTOs.Admin.HR
{
    public class AttendanceOverviewDto
    {
        public string Date { get; set; }
        public string? TimeIn { get; set; }
        public string? TimeOut { get; set; }
        public string Day {  get; set; }
    }
}