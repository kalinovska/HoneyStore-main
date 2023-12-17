namespace HoneyStore.Api.ViewModels
{
    public class OrderCreationModel
    {
        public int? UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Details { get; set; }

        public string DeliveryMethod { get; set; }

        public string PaymentMethod { get; set; }

        public int[] CartItemIds { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
