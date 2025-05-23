namespace LMS.Domain.Entities.HttpEntities
{
    public class ImgeURToken
    {
        public Guid ImgeURTokenId { get; set; }

        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;
    }
}