using LMS.Domain.Entities.Users;

namespace LMS.Domain.Entities.Orders
{
    public class Cart
    {
        // Primary key:
        public Guid CartId { get; set; }


        //Foreign Key: CustomerId ==> one(Cart)-to-one(Customer) relationship
        public Guid CustomerId { get; set; }


        public bool IsCheckedOut { get; set; }


        //Timestamp:
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        // Navigation property:
        public Customer Customer { get; set; }
        public ICollection<CartItem> CartItems { get; set; }


        public Cart()
        {
            CartId = Guid.NewGuid();
            IsCheckedOut = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Customer = null!;
            CartItems = new List<CartItem>();
        }
    }
}
