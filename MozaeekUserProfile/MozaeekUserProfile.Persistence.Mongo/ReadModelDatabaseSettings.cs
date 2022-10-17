namespace MozaeekUserProfile.Persistence.Mongo
{
    public class ReadModelDatabaseSettings : IReadModelDatabaseSettings
    {
        public string LabelsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}