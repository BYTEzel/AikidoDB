using AikidoTrainingDatabase.ApplicationLayer;
using System.IO;
using AikidoTrainingDatabase.Infrastructure.ExtendedClasses;
using System.Threading.Tasks;


namespace AikidoTrainingDatabase.Infrastructure.IO
{
    class XmlHandler : IDatabaseIO
    {

        public XmlHandler()
        {
        }

        public string GetDatabasePathExtension()
        {
            return "database.xml";
        }

        public IDatabase ReadDatabase(string path)
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(Database));
            var file = File.OpenRead(path);
            Database database = reader.Deserialize(file) as Database;
            file.Dispose();
            return database;
            //return await databaseXml.GetDatabase();
        }

        public void WriteDatabase(IDatabase database, string path)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Database));
            //await databaseXml.SetDatabase(database);

            FileStream file = File.Create(path);
            writer.Serialize(file, database);
            file.Dispose();
        }
    }
}
