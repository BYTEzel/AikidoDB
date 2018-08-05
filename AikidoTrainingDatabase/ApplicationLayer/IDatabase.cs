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

        /// <summary>
        /// Adds a specific excercise to the training list.
        /// </summary>
        /// <param name="training">Training which should contain the excercise.</param>
        /// <param name="excercise">Excercise which should be added to the given training.</param>
        void AddExcercise(ITraining training, IExcercise excercise);
        void Create(ICategory category);
        void Create(IExcercise excercise);
        void Create(ITraining training);
        void Edit(ICategory category);
        void Edit(IExcercise excercise);
        void Edit(ITraining training);
        void Delete(ICategory category);
        void Delete(IExcercise excercise);
        void Delete(ITraining training);
        /// <summary>
        /// Deletes a specific exercise of the training.
        /// </summary>
        /// <param name="training">Training which should be altered.</param>
        /// <param name="excerciseIndex">Index of the excercise which should be deleted.</param>
        void Delete(ITraining training, int excerciseIndex);
    }
}
