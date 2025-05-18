namespace LMS.Common.Settings
{
    public class TokenSettings
    {
        public string SecretKey { get; set; }
        public string Issure { get; set; }
        public string Audience { get; set; }
        public int AccessTokenExpiryMinutes { get; set; }
    }
}