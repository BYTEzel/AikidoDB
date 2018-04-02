using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;

namespace AikidoTrainingDatabase.Domain
{
    public interface IExcercise : ISearchable
    {
        string Name { get; set; }
        string Description { get; set; }

        ObservableCollection<Category> Categories { get; set; }
        ObservableCollection<BitmapImage> Images { get; set; }
    }
}
