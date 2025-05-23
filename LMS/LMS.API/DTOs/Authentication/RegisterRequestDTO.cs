using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.DTOs.Authentication
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(60, ErrorMessage = "Max lenght is 60 character")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "User Is Required")]
        [MaxLength(60, ErrorMessage = "Max lenght is 60 character")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "User Is Required")]
        [MaxLength(23, ErrorMessage = "Max lenght is 60 character")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "you must provide a valid number")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password Is Required")]
        [MaxLength(60, ErrorMessage = "Max lenght is 60 character")]
        [MinLength(8, ErrorMessage = "Min lenght is 8 character")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Language is required")]
        public int Language { get; set; } = 0;

        [FromForm]
        public IFormFile? ProfilePecture { get; set; }
    }
}
