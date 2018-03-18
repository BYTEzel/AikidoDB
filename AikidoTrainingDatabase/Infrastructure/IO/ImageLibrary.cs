using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;

namespace AikidoTrainingDatabase.Infrastructure.IO
{
    public class ImageLibrary
    {
        List<ImageBase64> imageLibrary;

        public ImageLibrary()
        {
            imageLibrary = new List<ImageBase64>();
        }

        public void AddEntry(BitmapImage bitmapImage, string base64)
        {
            imageLibrary.Add(new ImageBase64(bitmapImage, base64));
        }

        public void ClearAll()
        {
            imageLibrary.Clear();
        }

        public bool CheckEntry(BitmapImage bitmapImage)
        {
            foreach (ImageBase64 ib in imageLibrary)
            {
                if (bitmapImage == ib.bitmapImage)
                    return true;
            }
            return false;
        }

        public string FindEntry(BitmapImage bitmapImage)
        {
            foreach (ImageBase64 ib in imageLibrary)
            {
                if (bitmapImage == ib.bitmapImage)
                    return ib.base64;
            }
            return string.Empty;
        }
    }
}
