namespace HoneyStore.Api.ViewModels
{
    public class WishCreationModel
    {
        public int UserId { get; set; }

        public int Quantity { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public int ProductId { get; set; }
    }
}
