using System;
using AikidoTrainingDatabase.ApplicationLayer;
using Windows.Storage;
using System.IO;

namespace AikidoTrainingDatabase.Infrastructure.IO
{
    class XmlHandler : IDatabaseIO
    {
        string localFolder;

        public XmlHandler()
        {
            localFolder = (ApplicationData.Current.LocalFolder as StorageFolder).Path + "\\";
        }

        public IDatabase ReadDatabase(string fileName)
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(Database));
            var file = File.OpenRead(localFolder + fileName);
            Database database = reader.Deserialize(file) as Database;
            file.Dispose();
            return database;
        }

        public bool WriteDatabase(IDatabase database, string fileName)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Database));

            try
            {
                FileStream file = File.Create(localFolder + fileName);
                writer.Serialize(file, database);
                file.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
