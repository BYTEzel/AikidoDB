using AikidoTrainingDatabase.ApplicationLayer;
using System.Threading.Tasks;

namespace AikidoTrainingDatabase.Infrastructure.IO
{
    interface IDatabaseIO
    {
        string GetDatabasePathExtension();
        Task<IDatabase> ReadDatabase(string path);
        Task WriteDatabase(IDatabase database, string fileName);
    }
}
