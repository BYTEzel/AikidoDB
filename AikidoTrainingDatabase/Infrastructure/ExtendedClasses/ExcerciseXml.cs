using AikidoTrainingDatabase.Domain;
using AikidoTrainingDatabase.Infrastructure.IO;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace AikidoTrainingDatabase.Infrastructure.ExtendedClasses
{
    /// <summary>
    /// Holds similar fields to Excercise but transform the Excercise in a way that the image
    /// can be stored in XML.
    /// </summary>
    public class ExcerciseXml
    {
        public string Name;
        public string Description;
        public ObservableCollection<Category> Categories;
        public ObservableCollection<string> Images;

        private UriHandler uriHandler;

        public ExcerciseXml()
        {
            Name = string.Empty;
            Description = string.Empty;
            Categories = new ObservableCollection<Category>();
            Images = new ObservableCollection<string>();
            uriHandler = new UriHandler();
        }
        
        /// <summary>
        /// Converts the values of an excercise into the new representation.
        /// Most significant, the image is converted internally to a string
        /// which can be interpreted by XML.
        /// </summary>
        /// <param name="excercise"></param>
        public async Task SetExcercise(IExcercise excercise)
        {
            Name = excercise.Name;
            Description = excercise.Description;
            Categories = excercise.Categories;
            Images = new ObservableCollection<string>();
            // Convert the image to string-based format
            foreach (BitmapImage img in excercise.Images)
            {
                Images.Add(await ImageToString(img));
            }
        }

        public async Task<Excercise> GetExerciseAsync()
        {
            var bitmapImages = new ObservableCollection<BitmapImage>();
            foreach(string s in Images)
            {
                bitmapImages.Add(await StringToImage(s));
            }
            return new Excercise(Name, Description, Categories, bitmapImages);
        }
        
        
        /// <summary>
        /// Convert a bitmap image to a base64 string representation which should be 
        /// able to be written/read from XML.
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
        private async Task<string> ImageToString(BitmapImage bitmapImage)
        {   
            if (uriHandler.checkUri(bitmapImage.UriSource))
            {
                // The file was already converted, get the information directly.
                return uriHandler.decodeStringInfo(bitmapImage.UriSource);
            }
            else
            {
                // The file is freshly loaded from HDD, no previous conversation 
                // available.
                string base64string;
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(bitmapImage.UriSource);
                using (var inputStream = await file.OpenSequentialReadAsync())
                {
                    var readStream = inputStream.AsStreamForRead();

                    var byteArray = new byte[readStream.Length];
                    await readStream.ReadAsync(byteArray, 0, byteArray.Length);
                    base64string = Convert.ToBase64String(byteArray);
                }
                return base64string;
            }
        }

        
        /// <summary>
        /// Convert a base64 string to a image.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>        
        private async Task<BitmapImage> StringToImage(string s)
        {
            byte[] bytes = Convert.FromBase64String(s);
            BitmapImage image = new BitmapImage();
            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                await stream.WriteAsync(bytes.AsBuffer());
                stream.Seek(0);
                await image.SetSourceAsync(stream);
            }
            image.UriSource = uriHandler.encodeStringInfo(s);

            return image;
        }
    }
}
