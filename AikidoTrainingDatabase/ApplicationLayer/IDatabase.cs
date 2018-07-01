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
        ObservableCollection<Training> TrainingList { get; set; }
        IdDatabase TrainingId { get; set; }
        
        void Create(ICategory category);
        void Create(IExcercise excercise);
        void Create(ITraining training);
        void Edit(ICategory category);
        void Edit(IExcercise excercise);
        void Edit(ITraining training);
        void Delete(ICategory category);
        void Delete(IExcercise excercise);
        void Delete(ITraining training);
    }
}
