using AikidoTrainingDatabase.Domain;
using System.Collections.ObjectModel;

namespace AikidoTrainingDatabase.ApplicationLayer
{
    /// <summary>
    /// Collects the data which is currently present in the script.
    /// </summary>
    public class Database : IDatabase
    {
        private ObservableCollection<Category> categoryList;

        public Database()
        {
            categoryList = new ObservableCollection<Category>();
        }

        public ObservableCollection<Category> CategoryList { get => categoryList; }

        public void Create(ICategory category)
        {
            categoryList.Add(category as Category);
        }

        public void Delete(ICategory category)
        {
            categoryList.Remove(category as Category);
        }

        public void Edit(ICategory category, int index)
        {
            categoryList.RemoveAt(index);
            categoryList.Insert(index, category as Category);
        }
    }
}
