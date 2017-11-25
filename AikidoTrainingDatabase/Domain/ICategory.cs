using System.Collections.ObjectModel;

namespace AikidoTrainingDatabase.Domain
{
    public interface ICategory
    {
        string Name { get; set; }
        string Description { get; set; }
    }
}
