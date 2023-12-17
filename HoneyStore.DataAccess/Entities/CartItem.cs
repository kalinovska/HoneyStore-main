using HoneyStore.DataAccess.Interfaces;

namespace HoneyStore.DataAccess.Entities
{
    public class CartItem: IIdentifier
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int Quantity { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int? OrderId { get; set; }
    }
}