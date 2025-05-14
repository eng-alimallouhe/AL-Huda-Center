using LMS.Domain.Entities.Users;

namespace LMS.Domain.Entities.Orders
{
    public class Shipment : Order
    {
        //Foreign Key: AddressId ==> one(Address)-to-one(PrintOrder) relationship
        public Guid AddressId { get; set; }


        public Address Address { get; set; }

        public Shipment()
        {
            Address = null!;
            base.DeliveryMethod = Enums.Orders.DeliveryMethod.HomeDelivery;
        }
    }
}
