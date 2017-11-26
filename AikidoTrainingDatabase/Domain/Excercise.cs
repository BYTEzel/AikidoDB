using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;

namespace AikidoTrainingDatabase.Domain
{
    public class Excercise : IExcercise
    {
        private string name;
        private string description;
        private ObservableCollection<Category> categories;
        private ObservableCollection<BitmapImage> images;

        #region Getter/setter
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public ObservableCollection<Category> Categories { get => categories; set => categories = value; }
        public ObservableCollection<BitmapImage> Images { get => images; set => images = value; }
        #endregion

        #region Constructors
        public Excercise()
        {
            Name = string.Empty;
            Description = string.Empty;
            Categories = new ObservableCollection<Category>();
            Images = new ObservableCollection<BitmapImage>();
        }

        public Excercise(string name)
        {
            Name = name;
            Description = string.Empty;
            Categories = new ObservableCollection<Category>();
            Images = new ObservableCollection<BitmapImage>();
        }

        public Excercise(string name, string description)
        {
            Name = name;
            Description = description;
            Categories = new ObservableCollection<Category>();
            Images = new ObservableCollection<BitmapImage>();
        }

        public Excercise(string name, string description, 
            ObservableCollection<Category> categories, ObservableCollection<BitmapImage> images)
        {
            Name = name;
            Description = description;
            Categories = categories;
            Images = images;
        }
        #endregion
    }
}
