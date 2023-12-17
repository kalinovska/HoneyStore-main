using System.Drawing;

namespace HoneyStore.Api.Helpers
{
    public interface IFileHelper
    {
        byte[] ImageToByteArray(Image image);

        Image ByteArrayToImage(byte[] file);
    }

    public class FileHelper : IFileHelper
    {
        public byte[] ImageToByteArray(Image image)
        {
            var stream = new MemoryStream();
            image.Save(stream, image.RawFormat);
            return stream.ToArray();
        }

        public Image ByteArrayToImage(byte[] file)
        {
            var stream = new MemoryStream(file);

            return Image.FromStream(stream);
        }
    }
}
