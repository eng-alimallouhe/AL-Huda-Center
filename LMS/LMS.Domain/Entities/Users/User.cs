﻿using LMS.Domain.Enums.Commons;

namespace LMS.Domain.Entities.Users
{
    public class User
    {
        //primary key
        public Guid UserId { get; set; }


        //Foreign Key: RoleId ==> one(role)-to-many(users) relationship
        public Guid RoleId { get; set; }
        public required string FullName { get; set; } 
        public required string UserName { get; set; } 
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public string HashedPassword { get; set; }
        public int FailedLoginAttempts { get; set; }
        public DateTime LastLogIn { get; set; }
        public DateTime? LockedUntil { get; set; }
        public string ProfilePictureUrl { get; set; }
        public DateTime LastPasswordChange { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
        public Language Language { get; set; } 

        //locke user account:
        public bool IsLocked { get; set; } = false;

        public bool IsEmailConfirmed { get; set; } = false;

        //soft delete:
        public bool IsDeleted { get; set; } = false;


        //Timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        
        //Navigation Property:
        public Role Role { get; set; }
        public OtpCode OtpCode { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public ICollection<Notification> Notifications { get; set; }

        public User()
        {
            UserId = Guid.NewGuid();
            HashedPassword = string.Empty;
            ProfilePictureUrl = "https://i.imgur.com/IKv7CE0.jpeg";
            FailedLoginAttempts = 0;
            LastLogIn = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Role = null!;
            OtpCode = null!;
            RefreshToken = null!;
            Notifications = [];
        }
    }
}
