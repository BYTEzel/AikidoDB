using AikidoTrainingDatabase.Domain;

namespace AikidoTrainingDatabase.ApplicationLayer
{
    public interface IApplication
    {
        void ShowCategories();
        void CreateCategory();
        void CreateCategoryCallback(ICategory category);
        bool VerifyCategory(ICategory category);
        void EditCategory(ICategory categoryToEdit, int index);
        void EditCategoryCallback(ICategory categoryEdited, int index);
        void DeleteCategory(ICategory category);
        void WriteDatabase();
        void ReadDatabase();
    }
}
