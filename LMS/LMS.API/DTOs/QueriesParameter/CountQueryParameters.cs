using LMS.Domain.Enums.Commons;

namespace LMS.API.DTOs.QueriesParameter
{
    public class CountQueryParameters
    {
        public int TopCount { get; set; }

        public Language Language { get; set; }
    }
}
