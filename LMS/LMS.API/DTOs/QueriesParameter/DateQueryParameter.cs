using System.ComponentModel.DataAnnotations;

namespace LMS.API.DTOs.QueriesParameter
{
    public class DateQueryParameter
    {
        [DataType(DataType.Date)]
        public DateTime From { get; set; }

        [DataType(DataType.Date)]
        public DateTime To { get; set; }
    }
}
