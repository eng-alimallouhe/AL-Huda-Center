namespace LMS.Application.DTOs.AuthenticationDTOs
{
    public class AuthorizationDTO
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}