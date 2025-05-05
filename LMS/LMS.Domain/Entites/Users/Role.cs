namespace LMS.Domain.Entites.Users
{
    public class Role
    {
       //Primary key:
        public Guid RoleId { get; set; }


        public required string RoleType { get; set; }
        public required string RoleDescription { get; set; }

        //Soft Delete:
        public bool IsActive { get; set; }

        //Timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Role()
        {
            RoleId = Guid.NewGuid();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
