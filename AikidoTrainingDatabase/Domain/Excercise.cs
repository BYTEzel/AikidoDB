using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;

namespace AikidoTrainingDatabase.Domain
{
    class Excercise : IExcercise
    {
        private string name;
        private string description;
        private ObservableCollection<ICategory> categories;
        private ObservableCollection<BitmapImage> images;

        #region Getter/setter
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public ObservableCollection<ICategory> Categories { get => categories; set => categories = value; }
        public ObservableCollection<BitmapImage> Images { get => images; set => images = value; }
        #endregion
    }
}
