using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;

namespace AikidoTrainingDatabase.Domain
{
    public interface IExcercise : ISearchable
    {
        ObservableCollection<Category> Categories { get; set; }
        ObservableCollection<BitmapImage> Images { get; set; }
    }
}
