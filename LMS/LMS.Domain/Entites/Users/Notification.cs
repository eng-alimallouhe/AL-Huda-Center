using LMS.Domain.Enums.Users;

namespace LMS.Domain.Entities.Users
{
    public class Notification
    {
        //primary key: 
        public Guid NotificationId { get; set; }


        // Foreign Key: UserId ==> one(user) to many(notifications) relationship
        public Guid UserId { get; set; }
        
        
        public required string Title { get; set; }
        public required string Message { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ReadAt { get; set; }
        public string RedirectUrl { get; set; }
        public NotificationType NotificationType { get; set; }
        public NotificationPriority NotificationPriority { get; set; }



        public Notification()
        {
            NotificationId = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            IsRead = false;
            NotificationDate = DateTime.UtcNow;
            RedirectUrl = string.Empty;
        }
    }
}
