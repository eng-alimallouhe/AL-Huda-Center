using System.Globalization;
using AutoMapper;
using LMS.Application.DTOs.Admin.HR;
using LMS.Domain.Entities.HR;

namespace LMS.Application.MappingProfiles.HR.MappingSettings
{
    public class AttendanceToOverviewConverter : ITypeConverter<Attendance, AttendanceOverviewDto>
    {
        public AttendanceOverviewDto Convert(Attendance source, AttendanceOverviewDto destination, ResolutionContext context)
        {
            var lang = context.Items.ContainsKey("Language") ? context.Items["Language"]?.ToString() : "en";

            return new AttendanceOverviewDto
            {
                Date = source.Date.ToString("yyyy-MM-dd"),
                TimeIn = (source.TimeIn is null) ? "N/A" : source.TimeIn.Value.ToString(@"hh\:mm"),
                TimeOut = (source.TimeOut is null) ? "N/A" : source.TimeOut.Value.ToString(@"hh\:mm"),
                Day = GetDayName(source.Date, lang!)
            };
        }

        private string GetDayName(DateTime date, string language)
        {
            var culture = language == "ar" ? new CultureInfo("ar-SA") : CultureInfo.InvariantCulture;
            return culture.DateTimeFormat.GetDayName(date.DayOfWeek);
        }

    }
}
