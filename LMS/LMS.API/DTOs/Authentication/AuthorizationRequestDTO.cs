using System.ComponentModel.DataAnnotations;

namespace LMS.API.DTOs.Authentication
{
    public class AuthorizationRequestDTO
    {
        [Required(ErrorMessage = "Refresh token is required")]
        public string RefreshToken { get; set; }

        [Required(ErrorMessage = "Access token is required")]
        public string AccessToken { get; set; }
    }
}
