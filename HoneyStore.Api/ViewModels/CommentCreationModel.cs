namespace HoneyStore.Api.ViewModels
{
    public class CommentCreationModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public double Mark { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
