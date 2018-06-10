using AikidoTrainingDatabase.Domain;
using System.Collections.ObjectModel;

namespace AikidoTrainingDatabase.ApplicationLayer
{
    public interface IApplication
    {
        void CreateDatabase(string PathDb);
        void CreateCategory();
        void CreateExcercise();
        void CreateCategoryCallback(ICategory category);
        void CreateExcerciseCallback(IExcercise excercise);
        void ShowCategories();
        void ShowExcercises();
        bool VerifyCategory(ICategory category);
        bool VerifyExcercise(IExcercise excercise);
        void EditCategory(ICategory categoryToEdit);
        void EditExcercise(IExcercise excerciseToEdit);
        void EditCategoryCallback(ICategory categoryEdited);
        void EditExcerciseCallback(IExcercise excerciseEdited);
        void DeleteCategory(ICategory category);
        void DeleteExcercise(IExcercise excercise);
        string GetDatabasePathExtension();
        ObservableCollection<Category> GetCategories();
        ObservableCollection<Excercise> GetExcercises();
        void WriteDatabase();
        void WriteDatabase(string PathDb);
        void ReadDatabase(string PathDb);
    }
}
