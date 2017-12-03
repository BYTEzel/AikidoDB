using AikidoTrainingDatabase.Domain;
using System.Collections.ObjectModel;

namespace AikidoTrainingDatabase.ApplicationLayer
{
    public interface IDatabase
    {
        ObservableCollection<Category> CategoryList { get; set; }
        ObservableCollection<Excercise> ExcerciseList { get; set; }

        void Create(ICategory category);
        void Create(IExcercise excercise);
        void Edit(ICategory category, int index);
        void Edit(IExcercise excercise, int index);
        void Delete(ICategory category);
        void Delete(IExcercise excercise);
    }
}
