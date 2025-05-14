using LMS.Domain.Enums.Users;

namespace LMS.Domain.Entities.Users
{
    public class OtpCode
    {
        //Primary Key:
        public Guid OtpCodeId { get; set; }
        
        //Foreign Key: UserId ==> one(user) to one(OtpCode) relationship
        public Guid UserId { get; set; }
        
        public required string HashedValue { get; set; }
        public bool IsUsed { get; set; }
        public int FailedAttempts { get; set; }
        public CodeType CodeType { get; set; }


        //Timestamp:
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ExpiredAt { get; set; } = DateTime.Now.AddMinutes(10);
        

        public OtpCode()
        {
            OtpCodeId = Guid.NewGuid();
        }
    }
}
