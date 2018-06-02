using AikidoTrainingDatabase.Domain;
using System.Collections.ObjectModel;

namespace AikidoTrainingDatabase.ApplicationLayer
{
    public interface IDatabase
    {
        ObservableCollection<Category> CategoryList { get; set; }
        IdDatabase CategoryId { get; set; }
        ObservableCollection<Excercise> ExcerciseList { get; set; }
        IdDatabase ExcerciseId { get; set; }
        
        void Create(ICategory category);
        void Create(IExcercise excercise);
        void Edit(ICategory category);
        void Edit(IExcercise excercise);
        void Delete(ICategory category);
        void Delete(IExcercise excercise);
    }
}
