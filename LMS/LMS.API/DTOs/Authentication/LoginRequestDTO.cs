using System.ComponentModel.DataAnnotations;

namespace LMS.API.DTOs.Authentication
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "you must provide a valid number")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; } = string.Empty;
    }
}
