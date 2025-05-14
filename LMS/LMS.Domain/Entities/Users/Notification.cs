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
        public DateTime CreatedAt { get; set; } 
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
        public string? RedirectUrl { get; set; }

        

        public Notification()
        {
            NotificationId = Guid.NewGuid();
            IsRead = false;
        }

    }
}
