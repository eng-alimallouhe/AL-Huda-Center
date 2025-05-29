using System.ComponentModel.DataAnnotations;
using LMS.Domain.Enums.Users;

namespace LMS.API.DTOs.Admin.Department
{
    public class DepartmentRequestDto
    {
        [Required]
        public string DepartmentName { get; set; }


        [Required]
        public string DepartmentDescription { get; set; }


        [Required]
        public ResponsibilityType ResponsibilityType { get; set; }
    }
}