using LMS.Domain.Entities.Users;
using LMS.Domain.Enums.Orders;

namespace LMS.Domain.Entities.Orders
{
    public class Shipment
    {
        //Primary Key:
        public Guid ShipmentId { get; set; }


        //Foreign Key: OrderId ==> one(order)-to-one(shipment) relationship
        public Guid OrderId { get; set; }


        //Foreign Key: AddressId ==> one(address)-to-one(shipment) relationship
        public Guid AddressId { get; set; }


        //Foreign Key: CustomerId ==> one(customer)-to-many(order) relationship
        public Guid CustomerId { get; set; }


        //Foreign Key: EmployeeId ==> one(employee)-to-many(order) relationship
        public Guid EmployeeId { get; set; }


        public string TrackingNumber { get; set; }
        public decimal Cost { get; set; }
        public ShipmentStatus Status { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public bool IsDelivered { get; set; }


        //Soft delete:
        public bool IsActive { get; set; }


        // Navigation
        public Order Order { get; set; }
        public Address Address { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }

        public Shipment()
        {
            ShipmentId = Guid.NewGuid();
            IsActive = true;
            TrackingNumber = string.Empty;
            Order = null!;
            Address = null!;
            Customer = null!;
            Employee = null!;
        }
    }
}