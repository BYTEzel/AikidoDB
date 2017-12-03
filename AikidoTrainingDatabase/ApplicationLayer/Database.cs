using AikidoTrainingDatabase.Domain;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;

namespace AikidoTrainingDatabase.ApplicationLayer
{
    /// <summary>
    /// Collects the data which is currently present in the script.
    /// </summary>
    public class Database : IDatabase
    {
        private ObservableCollection<Category> categoryList;
        private ObservableCollection<Excercise> excerciseList;

        public Database()
        {
            categoryList = new ObservableCollection<Category>();
            excerciseList = new ObservableCollection<Excercise>();

            var categories = new ObservableCollection<Category>();
            categories.Add(new Category("Cat1"));
            categories.Add(new Category("Cat2"));

            var images = new ObservableCollection<BitmapImage>();
            images.Add(new BitmapImage(new System.Uri("ms-appx://AikidoTrainingDatabase/Assets/StoreLogo.png")));

            excerciseList.Add(new Excercise("Name", "Description"));
            excerciseList.Add(new Excercise("Name2", "Description2", categories, images));
        }

        public ObservableCollection<Category> CategoryList { get => categoryList; set => categoryList = value;  }
        public ObservableCollection<Excercise> ExcerciseList { get => excerciseList; set => excerciseList = value; }

        public void Create(ICategory category)
        {
            categoryList.Add(category as Category);
        }

        public void Create(IExcercise excercise)
        {
            excerciseList.Add(excercise as Excercise);
        }

        public void Delete(ICategory category)
        {
            categoryList.Remove(category as Category);
        }

        public void Delete(IExcercise excercise)
        {
            excerciseList.Remove(excercise as Excercise);
        }

        public void Edit(ICategory category, int index)
        {
            categoryList[index] = category as Category;
        }

        public void Edit(IExcercise excercise, int index)
        {
            excerciseList[index] = excercise as Excercise;
        }
    }
}
