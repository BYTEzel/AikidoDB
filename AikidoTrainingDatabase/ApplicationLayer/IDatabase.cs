using AikidoTrainingDatabase.Domain;
using System.Collections.ObjectModel;

namespace AikidoTrainingDatabase.ApplicationLayer
{
    public interface IDatabase
    {
        ObservableCollection<Category> CategoryList { get; }
        void Create(ICategory category);
        void Edit(ICategory category, int index);
        void Delete(ICategory category);
    }
}
