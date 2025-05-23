using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.DTOs.Admin.Employee
{
    public class EmployeeCreateRequestDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }


        [Required]
        public int Language { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }


        [FromForm]
        public IFormFile? ProfilePicture { get; set; }
    }
}
