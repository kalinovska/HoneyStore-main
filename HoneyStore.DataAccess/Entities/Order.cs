using HoneyStore.DataAccess.Identity;
using HoneyStore.DataAccess.Interfaces;

namespace HoneyStore.DataAccess.Entities
{
    public class Order : IIdentifier
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Details { get; set; }

        public string DeliveryMethod { get; set; }

        public string PaymentMethod { get; set; }

        public User User { get; set; }

        public DateTime CreatedOn { get; set; }

        public OrderStatus Status { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }
}