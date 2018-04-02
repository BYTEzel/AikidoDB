using AikidoTrainingDatabase.ApplicationLayer;
using System.IO;
using AikidoTrainingDatabase.Infrastructure.ExtendedClasses;
using System.Threading.Tasks;


namespace AikidoTrainingDatabase.Infrastructure.IO
{
    class XmlHandler : IDatabaseIO
    {
        DatabaseXml databaseXml;

        public XmlHandler()
        {
            databaseXml = new DatabaseXml();
        }

        public string GetDatabasePathExtension()
        {
            return "database.xml";
        }

        public async Task<IDatabase> ReadDatabase(string path)
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(DatabaseXml));
            var file = File.OpenRead(path);
            databaseXml = reader.Deserialize(file) as DatabaseXml;
            file.Dispose();
            return await databaseXml.GetDatabase();
        }

        public async Task WriteDatabase(IDatabase database, string path)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(DatabaseXml));
            await databaseXml.SetDatabase(database);

            FileStream file = File.Create(path);
            writer.Serialize(file, databaseXml);
            file.Dispose();
        }
    }
}
