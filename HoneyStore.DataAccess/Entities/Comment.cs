using HoneyStore.DataAccess.Identity;
using HoneyStore.DataAccess.Interfaces;

namespace HoneyStore.DataAccess.Entities
{
    public class Comment : IIdentifier
    {
        public int Id { get; set; }
        
        public int Mark { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
