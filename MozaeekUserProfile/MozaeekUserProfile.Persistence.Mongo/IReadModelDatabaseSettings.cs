namespace MozaeekUserProfile.Persistence.Mongo
{
    public interface IReadModelDatabaseSettings
    {
        string LabelsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}