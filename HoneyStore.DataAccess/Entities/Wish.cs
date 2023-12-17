using HoneyStore.DataAccess.Identity;
using HoneyStore.DataAccess.Interfaces;

namespace HoneyStore.DataAccess.Entities
{
    public class Wish : IIdentifier
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
