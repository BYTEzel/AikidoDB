using System.Collections.ObjectModel;

namespace AikidoTrainingDatabase.Domain
{
    public interface ITraining : ISearchable, IID
    {
        string Name { get; set; }
        string Description { get; set; }

        ObservableCollection<Excercise> Excercises { get; set; }
    }
}
