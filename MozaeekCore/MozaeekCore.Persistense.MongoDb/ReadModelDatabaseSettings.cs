using System.Collections.ObjectModel;

namespace MozaeekCore.Persistense.MongoDb
{
    public class ReadModelDatabaseSettings : IReadModelDatabaseSettings
    {
        public string LabelsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IReadModelDatabaseSettings
    {
        string LabelsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
