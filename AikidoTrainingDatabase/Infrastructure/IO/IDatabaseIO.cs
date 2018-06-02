using AikidoTrainingDatabase.ApplicationLayer;
using System.Threading.Tasks;

namespace AikidoTrainingDatabase.Infrastructure.IO
{
    interface IDatabaseIO
    {
        string GetDatabasePathExtension();
        IDatabase ReadDatabase(string path);
        void WriteDatabase(IDatabase database, string fileName);
    }
}
