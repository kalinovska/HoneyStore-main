namespace HoneyStore.Api.ViewModels
{
    public class CartItemCreationModel
    {
        public int UserId { get; set; }

        public int Quantity { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public int ProductId { get; set; }
        
        public int? OrderId { get; set; }
    }
}
