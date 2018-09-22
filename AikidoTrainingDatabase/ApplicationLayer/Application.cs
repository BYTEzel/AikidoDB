using AikidoTrainingDatabase.Domain;
using AikidoTrainingDatabase.Infrastructure;
using AikidoTrainingDatabase.Infrastructure.IO;
using AikidoTrainingDatabaseSql.HtmlExport;
using System.Collections.ObjectModel;
using System;
using System.Threading.Tasks;
using Windows.Storage;

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


        #region Create
        public void AddExcerciseToTraining(ITraining training, IExcercise excercise)
        {
            database.AddExcercise(training, excercise);
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

        public void CreateTraining()
        {
            gui.ShowCreateTraining();
        }

        public void CreateTrainingCallback(ITraining training)
        {
            database.Create(training);
            ShowTrainings();
        }
        #endregion

        #region Delete
        public void DeleteCategory(ICategory category)
        {
            database.Delete(category);
        }

        public void DeleteExcercise(IExcercise excercise)
        {
            database.Delete(excercise);
        }

        public void DeleteTraining(ITraining training)
        {
            database.Delete(training);
        }

        public void DeleteExcerciseOfTraining(ITraining training, int excerciseIndex)
        {
            database.Delete(training, excerciseIndex);
        }
        #endregion

        #region Edit
        public void EditCategory(ICategory categoryToEdit)
        {
            gui.ShowEditCategory(categoryToEdit);
        }

        public void EditCategoryCallback(ICategory categoryEdited)
        {
            database.Edit(categoryEdited);
            ShowCategories();
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

        public void EditTraining(ITraining trainingToEdit)
        {
            gui.ShowEditTraining(trainingToEdit);
        }
        
        public void EditTrainingCallback(ITraining trainingEdited)
        {
            database.Edit(trainingEdited);
            ShowTrainings();
        }
        #endregion

        #region Show
        public void ShowCategories()
        {
            gui.ShowCategoryPage(database.CategoryList);
        }

        public void ShowCategorySingle(ICategory category)
        {
            gui.ShowCategorySingle(category);
        }

        public void ShowExcercises()
        {
            gui.ShowExcercisePage(database.ExcerciseList);
        }

        public void ShowExcerciseSingle(IExcercise excercise)
        {
            gui.ShowExcerciseSingle(excercise);
        }

        public void ShowTrainings()
        {
            gui.ShowTrainingPage(database.TrainingList);
        }

        public void ShowTrainingSingle(ITraining training)
        {
            gui.ShowTrainingSingle(training);
        }

        public void ShowCreateLoadDatabase()
        {
            gui.ShowCreateLoadDatabase();
        }
        #endregion

        #region Verify
        public bool VerifyCategory(ICategory category)
        {
            return VerifyName(category);
        }

        public bool VerifyExcercise(IExcercise excercise)
        {
            return VerifyName(excercise);
        }

        public bool VerifyTraining(ITraining training)
        {
            return VerifyName(training);
        }

        private bool VerifyName(ISearchable obj)
        {
            return (obj.Name != string.Empty) ? true : false;
        }
        #endregion

        #region Database 
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
        #endregion

        #region Export
        public async Task ExportTrainingAsync(ITraining training, string filename, StorageFolder storageFolder)
        {
            HtmlBuilderTraining htmlBuilder = new HtmlBuilderTraining(training);
            // Create sample file; replace if exists.
            StorageFile exportFile = await storageFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(exportFile, htmlBuilder.GetDocument());
        }
        #endregion


        #region Getter
        public ObservableCollection<Category> GetCategories()
        {
            return database.CategoryList;
        }

        public ObservableCollection<Excercise> GetExcercises()
        {
            return database.ExcerciseList;
        }
        
        public ObservableCollection<Training> GetTrainings()
        {
            return database.TrainingList;
        }
        #endregion
    }
}
