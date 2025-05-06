using LMS.Domain.Entities.Users;
using LMS.Domain.Enums.Orders;

namespace LMS.Domain.Entities.Orders
{
    public class BaseOrder
    {
        //primary key:
        public Guid BaseOrderId { get; set; }


        //Foreign Key: CustomerId ==> one(customer)-to-many(order) relationship
        public Guid CustomerId { get; set; }


        //Foreign Key: EmployeeId ==> one(employee)-to-many(order) relationship
        public Guid EmployeeId { get; set; }


        //Foreign Key: DepartmentId ==> one(department)-to-many(order) relationship
        public Guid DepartmentId { get; set; }



        public OrderStatus Status { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public decimal Cost { get; set; }



        //soft delete:
        public bool IsActive { get; set; }


        //timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        //navigation property:
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public Department Department { get; set; }


        public BaseOrder()
        {
            BaseOrderId = Guid.NewGuid();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Customer = null!;
            Employee = null!;
            Department = null!;
        }
    }
}
