namespace HoneyStore.BusinessLogic.Models
{
    public class CommentDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double Mark { get; set; }
        
        public string Content { get; set; }
        
        public DateTime CreatedOn { get; set; }
    }
}