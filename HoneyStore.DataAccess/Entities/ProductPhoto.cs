namespace HoneyStore.DataAccess.Entities
{
    public class ProductPhoto
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public byte[] FileBytes { get; set; }
    }
}
