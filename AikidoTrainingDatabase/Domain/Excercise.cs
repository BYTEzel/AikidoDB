using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;

namespace AikidoTrainingDatabase.Domain
{
    public class Excercise : IExcercise
    {
        private int id;
        private string name;
        private string description;
        private ObservableCollection<Category> categories;

        private const int INVALID_ID = -1;

        #region Getter/setter
        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public ObservableCollection<Category> Categories { get => categories; set => categories = value; }
        #endregion

        #region Constructors
        public Excercise()
        {
            id = INVALID_ID;
            Name = string.Empty;
            Description = string.Empty;
            Categories = new ObservableCollection<Category>();
        }

        public Excercise(string name)
        {
            id = INVALID_ID;
            Name = name;
            Description = string.Empty;
            Categories = new ObservableCollection<Category>();
        }

        public Excercise(string name, string description)
        {
            id = INVALID_ID;
            Name = name;
            Description = description;
            Categories = new ObservableCollection<Category>();
        }

        public Excercise(string name, string description, 
            ObservableCollection<Category> categories)
        {
            id = INVALID_ID;
            Name = name;
            Description = description;
            Categories = categories;
        }

        public Excercise(int id, string name)
        {
            this.id = id;
            Name = name;
            Description = string.Empty;
            Categories = new ObservableCollection<Category>();
        }

        public Excercise(int id, string name, string description)
        {
            this.id = id;
            Name = name;
            Description = description;
            Categories = new ObservableCollection<Category>();
        }

        public Excercise(int id, string name, string description,
            ObservableCollection<Category> categories)
        {
            this.id = id;
            Name = name;
            Description = description;
            Categories = categories;
        }
        #endregion
    }
}
