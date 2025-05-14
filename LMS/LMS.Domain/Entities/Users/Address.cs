namespace LMS.Domain.Entities.Users
{
    public class Address
    {
        //Primary Key:
        public Guid AddressId { get; set; }


        //Foreign Key: RoleId ==> one(user)-to-many(address) relationship
        public Guid? CustomerId { get; set; }


        //Foreign Key: RoleId ==> one(order)-to-one(address) relationship
        public Guid OrderId { get; set; }


        public required string Latitude { get; set; } 
        public required string Longitude { get; set; }
        public required string Governorate { get; set; }
        public required string City { get; set; }
        public required string Details { get; set; }
        public required string PhoneNumber { get; set; }

        
        public Address()
        {
            AddressId = Guid.NewGuid();
        }

    }
}
