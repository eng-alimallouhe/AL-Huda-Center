namespace LMS.Common.Settings
{
    public class EmailSettings
    {
        public string FromEmail { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Host { get; set; } = default!;
        public int Port { get; set; }
        public string LogoUrl { get; set; } = default!;
    }
}
