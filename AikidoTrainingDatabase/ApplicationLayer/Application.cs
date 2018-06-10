using AikidoTrainingDatabase.Domain;
using AikidoTrainingDatabase.Infrastructure;
using AikidoTrainingDatabase.Infrastructure.IO;
using System.Collections.ObjectModel;
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
        private string pathDb;

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
        
        public void CreateCategory()
        {
            gui.ShowCreateCategory();
        }

        public void CreateCategoryCallback(ICategory category)
        {
            database.Create(category);
            ShowCategories();
        }

        public void CreateExcercise()
        {
            gui.ShowCreateExcercise();
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

        public void DeleteExcercise(IExcercise excercise)
        {
            database.Delete(excercise);
        }

        public void EditCategory(ICategory categoryToEdit)
        {
            gui.ShowEditCategory(categoryToEdit);
        }

        public void EditCategoryCallback(ICategory categoryEdited)
        {
            database.Edit(categoryEdited);
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

        public void CreateDatabase(string PathDb)
        {
            pathDb = PathDb;
            WriteDatabase(PathDb);
        }

        public void WriteDatabase()
        {
            WriteDatabase(pathDb);
        }

        public void WriteDatabase(string PathDb)
        {
            databaseIO.WriteDatabase(database, PathDb);
        }

        public void ReadDatabase(string PathDb)
        {
            pathDb = PathDb;
            database = databaseIO.ReadDatabase(PathDb);
        }

        public string GetDatabasePathExtension()
        {
            return databaseIO.GetDatabasePathExtension();
        }

        public void EditExcercise(IExcercise excerciseToEdit)
        {
            gui.ShowEditExcercise(excerciseToEdit);
        }

        public void EditExcerciseCallback(IExcercise excerciseEdited)
        {
            database.Edit(excerciseEdited);
            ShowExcercises();
        }

        public bool VerifyExcercise(IExcercise excercise)
        {
            return (excercise.Name != string.Empty) ? true : false;
        }

        public ObservableCollection<Category> GetCategories()
        {
            return database.CategoryList;
        }

        public ObservableCollection<Excercise> GetExcercises()
        {
            return database.ExcerciseList;
        }
    }
}
