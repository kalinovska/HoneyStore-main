using HoneyStore.BusinessLogic.Models;

namespace HoneyStore.Api.ViewModels
{
    public class ProductCreationModel
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

        public ProducerDto Producer { get; set; }

        public int? ProductPhotoId { get; set; }
        
        public int? CategoryId { get; set; }
    }
}