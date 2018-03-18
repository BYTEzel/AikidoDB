using AikidoTrainingDatabase.Domain;
using System.Threading.Tasks;

namespace AikidoTrainingDatabase.ApplicationLayer
{
    public interface IApplication
    {
        Task Init();
        void ShowCategories();
        void ShowExcercises();
        Task CreateDatabase(string PathDb);
        void CreateCategory();
        void CreateCategoryCallback(ICategory category);
        void CreateExcerciseCallback(IExcercise excercise);
        bool VerifyCategory(ICategory category);
        void EditCategory(ICategory categoryToEdit, int index);
        void EditCategoryCallback(ICategory categoryEdited, int index);
        void DeleteCategory(ICategory category);
        string GetDatabasePathExtension();
        Task WriteDatabase(string PathDb);
        Task ReadDatabase(string PathDb);
    }
}
