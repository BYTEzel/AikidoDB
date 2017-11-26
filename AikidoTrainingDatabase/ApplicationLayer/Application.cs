using AikidoTrainingDatabase.Domain;
using AikidoTrainingDatabase.Infrastructure;
using AikidoTrainingDatabase.Infrastructure.IO;
using System.Threading.Tasks;

/// <summary>
/// Has the logic to execute all possible use-cases.
/// </summary>
namespace AikidoTrainingDatabase.ApplicationLayer
{
    class Application : IApplication
    {
        private IGui gui;
        private IDatabase database;
        private IDatabaseIO databaseIO;
        private const string PATH_DB = "database.xml";

        /// <summary>
        /// Database is initialized with the stored values.
        /// </summary>
        /// <param name="gui"></param>
        public Application(IGui gui)
        {
            this.gui = gui;
            databaseIO = new XmlHandler();
            //ReadDatabase();
            database = new Database();
        }

        public void CreateCategory()
        {
            gui.RequestCategory();
        }

        public void CreateCategoryCallback(ICategory category)
        {
            database.Create(category);
            ShowCategories();
        }

        public void CreateExcerciseCallback(IExcercise excercise)
        {
            database.Create(excercise);
            ShowExcercises();
        }

        public void DeleteCategory(ICategory category)
        {
            database.Delete(category);
        }

        public void EditCategory(ICategory categoryToEdit, int index)
        {
            gui.ShowEditCategory(categoryToEdit, index);
        }

        public void EditCategoryCallback(ICategory categoryEdited, int index)
        {
            database.Edit(categoryEdited, index);
            ShowCategories();
        }
        
        public void ShowCategories()
        {
            gui.ShowCategoryPage(database.CategoryList);
        }

        public void ShowExcercises()
        {
            gui.ShowExcercisePage(database.ExcerciseList);
        }

        public bool VerifyCategory(ICategory category)
        {
            if (category.Name != string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void WriteDatabase()
        {
            var taskWrite = Task.Run(() => databaseIO.WriteDatabase(database, PATH_DB));
            taskWrite.Wait();
        }

        public void ReadDatabase()
        {
            var taskRead = Task.Run(() => database = databaseIO.ReadDatabase(PATH_DB));
            taskRead.Wait();
        }

    }
}
