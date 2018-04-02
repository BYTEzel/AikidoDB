using AikidoTrainingDatabase.Domain;
using System.Threading.Tasks;

namespace AikidoTrainingDatabase.ApplicationLayer
{
    public interface IApplication
    {
        Task CreateDatabase(string PathDb);
        void CreateCategory();
        void CreateExcercise();
        void CreateCategoryCallback(ICategory category);
        void CreateExcerciseCallback(IExcercise excercise);
        void ShowCategories();
        void ShowExcercises();
        bool VerifyCategory(ICategory category);
        void EditCategory(ICategory categoryToEdit, int index);
        void EditExcercise(IExcercise excerciseToEdit, int index);
        void EditCategoryCallback(ICategory categoryEdited, int index);
        void EditExcerciseCallback(IExcercise excerciseEdited, int index);
        void DeleteCategory(ICategory category);
        string GetDatabasePathExtension();
        Task WriteDatabase();
        Task WriteDatabase(string PathDb);
        Task ReadDatabase(string PathDb);
    }
}
