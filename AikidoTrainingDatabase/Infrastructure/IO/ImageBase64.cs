using Windows.UI.Xaml.Media.Imaging;

namespace AikidoTrainingDatabase.Infrastructure.IO
{
    public class ImageBase64
    {
        public BitmapImage bitmapImage;
        public string base64;

        public ImageBase64(BitmapImage bitmap, string base64)
        {
            bitmapImage = bitmap;
            this.base64 = base64;
        }
    }
}
