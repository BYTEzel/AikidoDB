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


        public Application()
        {
            databaseIO = new XmlHandler();
            database = new Database();
        }

        /// <summary>
        /// Database is initialized with the stored values.
        /// </summary>
        /// <param name="gui"></param>
        public Application(IGui gui)
        {
            this.gui = gui;
            databaseIO = new XmlHandler();
            database = new Database();
        }

        /// <summary>
        /// All costly operations are done here.
        /// </summary>
        /// <returns></returns>
        public async Task Init()
        {

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

        public async Task CreateDatabase(string PathDb)
        {
            await WriteDatabase(PathDb);
        }

        public async Task WriteDatabase(string PathDb)
        {
            //await System.Threading.Tasks.Task.Run(() => databaseIO.WriteDatabase(database, PATH_DB));
            await databaseIO.WriteDatabase(database, PathDb);
        }

        public async Task ReadDatabase(string PathDb)
        {
            database = await databaseIO.ReadDatabase(PathDb);
        }

        public string GetDatabasePathExtension()
        {
            return databaseIO.GetDatabasePathExtension();
        }
    }
}
