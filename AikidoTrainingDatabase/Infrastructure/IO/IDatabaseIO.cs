using AikidoTrainingDatabase.ApplicationLayer;

namespace AikidoTrainingDatabase.Infrastructure.IO
{
    interface IDatabaseIO
    {
        IDatabase ReadDatabase(string path);
        bool WriteDatabase(IDatabase database, string fileName);
    }
}
