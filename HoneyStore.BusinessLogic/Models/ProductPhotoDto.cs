namespace HoneyStore.BusinessLogic.Models
{
    public class ProductPhotoDto
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public byte[] FileBytes { get; set; }
    }
}
