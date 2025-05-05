namespace LMS.Domain.Entites.Users
{
    public class User
    {
        //Primary key:
        public Guid UserId { get; set; }


        //Foreign Key: RoleId ==> one(role)-to-many(user) relationship
        public Guid RoleId { get; set; }


        public required string FullName { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public string HashedPassword { get; set; }
        public string ProfilePictureUrl { get; set; }
        public int FailedLoginAttempts { get; set; }
        public bool IsTwoFactorEnabled { get; set; }


        //User locking and blocking:
        public bool IsLocked { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsBlocked { get; set; }

        //Timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime LastPasswordChange { get; set; }
        public DateTime? LockedUntil { get; set; }
        public DateTime? LastCodeSendedAt { get; set; }


        //Navigation Property:
        public Role Role { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<OtpCode> OtpCodes { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public RefreshToken RefreshToken { get; set; }


        public User()
        {
            UserId = Guid.NewGuid();
            Role = null!;
            IsBlocked = false;
            IsLocked = false;
            IsEmailConfirmed = false;
            HashedPassword = string.Empty;
            ProfilePictureUrl = string.Empty;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;    
            LastLogin = DateTime.UtcNow;
            Notifications = [];
            OtpCodes = [];
            Addresses = [];
            RefreshToken = null!;
        }
    }
}
