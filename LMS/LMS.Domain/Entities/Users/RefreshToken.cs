namespace LMS.Domain.Entities.Users
{
    public class RefreshToken
    {
        //primary key:
        public Guid RefreshTokenId { get; set; }


        //Foriegn key:
        public Guid UserId { get; set; }


        public required string Token { get; set; }
        public DateTime ExpiredAt { get; set; }
        public DateTime CreatedAt { get; set; }


        public RefreshToken()
        {
            RefreshTokenId = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            ExpiredAt = DateTime.UtcNow.AddDays(7);
        }
    }
}