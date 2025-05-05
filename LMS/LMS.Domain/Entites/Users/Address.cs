namespace LMS.Domain.Entites.Users
{
    public class Address
    {
        //Primary Key:
        public Guid AddressId { get; set; }


        //Foreign Key: UserId ==> one(user)-to-one(address) relationship
        public Guid? UserId { get; set; }


        public required string Latitude { get; set; }
        public required string Longitude { get; set; }
        public required string Governorate { get; set; }
        public required string City { get; set; }
        public required string StreetName { get; set; }
        public required string BuildingName { get; set; }
        public required string PhoneNumber { get; set; }


        public Address()
        {
            AddressId = Guid.NewGuid();
        }
    }
}
