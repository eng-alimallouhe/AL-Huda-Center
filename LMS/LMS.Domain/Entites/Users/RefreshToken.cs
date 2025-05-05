namespace LMS.Domain.Entites.Users
{
    public class RefreshToken
    {
        //primary key:
        public Guid RefreshTokenId { get; set; }

        
        //Foreign Key: UserId ==> one(user)-to-one(refreshToken) relationship
        public Guid UserId { get; set; }


        public required string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsRevoked { get; set; } = false;


        //Timestamp:
        public DateTime CreatedAt { get; set; }


        public RefreshToken()
        {
            RefreshTokenId = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Expiration = DateTime.UtcNow.AddDays(7);
        }
    }
}
