using AikidoTrainingDatabase.Domain;

namespace AikidoTrainingDatabase.ApplicationLayer
{
    public interface IApplication
    {
        void ShowCategories();
        void ShowExcercises();
        void CreateCategory();
        void CreateCategoryCallback(ICategory category);
        void CreateExcerciseCallback(IExcercise excercise);
        bool VerifyCategory(ICategory category);
        void EditCategory(ICategory categoryToEdit, int index);
        void EditCategoryCallback(ICategory categoryEdited, int index);
        void DeleteCategory(ICategory category);
        void WriteDatabase();
        void ReadDatabase();
    }
}
