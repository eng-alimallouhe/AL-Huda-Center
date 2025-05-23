using System.ComponentModel.DataAnnotations;

namespace LMS.API.Controllers.Authintication
{
    public class ResetPasswordDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
