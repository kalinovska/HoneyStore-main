using HoneyStore.DataAccess.Interfaces;

namespace HoneyStore.DataAccess.Entities
{
    public class Product : IIdentifier
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Mark { get; set; }

        public int Quantity { get; set; }

        public int Weight { get; set; }

        public bool CommentsEnabled { get; set; }

        public int ProducerId { get; set; }

        public Producer Producer { get; set; }

        public int? ProductPhotoId { get; set; }
        
        public ProductPhoto ProductPhoto { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
