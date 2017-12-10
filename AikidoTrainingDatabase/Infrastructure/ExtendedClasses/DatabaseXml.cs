using AikidoTrainingDatabase.ApplicationLayer;
using AikidoTrainingDatabase.Domain;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AikidoTrainingDatabase.Infrastructure.ExtendedClasses
{
    /// <summary>
    /// This class prepares the original database in a way that it can be stored in XML.
    /// This mainly includes changing images to a compatible data type.
    /// </summary>
    public class DatabaseXml
    {
        public ObservableCollection<Category> CategoryList { get; }
        public ObservableCollection<ExcerciseXml> ExcerciseList { get; }

        private IDatabase database;

        public DatabaseXml()
        {
            database = new Database();
            CategoryList = new ObservableCollection<Category>();
            ExcerciseList = new ObservableCollection<ExcerciseXml>();
        }
                
        public async Task SetDatabase(IDatabase database)
        {
            this.database = database;
            foreach (Excercise e in database.ExcerciseList)
            {
                ExcerciseXml eXml = new ExcerciseXml();
                await eXml.SetExcercise(e);
                ExcerciseList.Add(eXml);
            }
        }

        public async Task<IDatabase> GetDatabase()
        {
            database.CategoryList = CategoryList;
            database.ExcerciseList = new ObservableCollection<Excercise>();
            foreach (ExcerciseXml e in ExcerciseList)
            {
                database.ExcerciseList.Add(await e.GetExerciseAsync());
            }
            return database;
        }
    }
}
