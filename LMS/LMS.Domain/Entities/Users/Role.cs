namespace LMS.Domain.Entities.Users
{
    public class Role
    {
        //primary key
        public Guid RoleId { get; set; }


        public string RoleType { get; set; } = string.Empty;


        //Soft Delete:
        public bool IsActive { get; set; } = true;


        //Timestamp:
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        public Role()
        {
            RoleId = Guid.NewGuid();
        }
    }
}
