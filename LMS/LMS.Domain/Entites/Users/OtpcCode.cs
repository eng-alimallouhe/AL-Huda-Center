using LMS.Domain.Enums.Users;

namespace LMS.Domain.Entites.Users
{
    public class OtpCode
    {
        //primary Key:
        public Guid OtpCodeId { get; set; }


        //foreign Key: UserId ==> one(user) to one(OtpCode) relationship
        public Guid UserId { get; set; }


        public string HashedValue { get; set; }
        public int FailedAttempts { get; set; }
        public CodeType CodeType { get; set; }


        //timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }


        public OtpCode()
        {
            OtpCodeId = Guid.NewGuid();
            HashedValue = string.Empty;
            FailedAttempts = 0;
            CreatedAt = DateTime.UtcNow;
            ExpiredAt = DateTime.UtcNow.AddMinutes(15);
        }
    }
}
