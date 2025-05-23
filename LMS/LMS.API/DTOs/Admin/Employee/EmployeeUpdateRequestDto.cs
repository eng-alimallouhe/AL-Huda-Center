using System.ComponentModel.DataAnnotations;

namespace LMS.API.DTOs.Admin.Employee
{
    public class EmployeeUpdateRequestDto
    {
        [Required]
        public Guid EmployeeId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public decimal BaseSalary { get; set; }
    }
}
