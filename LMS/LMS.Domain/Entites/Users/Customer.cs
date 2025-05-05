namespace LMS.Domain.Entites.Users
{
    public class Customer : User
    {
        //Foreign Key: LevelId ==> one(customer)-to-one(level) relationship
        public Guid LevelId { get; set; }


        public decimal Points { get; set; }
    }
}
