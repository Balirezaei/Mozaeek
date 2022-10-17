using MongoDB.Driver;

namespace MozaeekUserProfile.Persistence.Mongo
{
    public class MongoRepository : IMongoRepository
    {
        public MongoRepository(IReadModelDatabaseSettings settings)
        {
            Settings = settings;
            var client = new MongoClient(settings.ConnectionString);
            Database = client.GetDatabase(settings.DatabaseName);
            LabelQueryCollection = Database.GetCollection<LabelQuery>("LabelCollection");
            PointQueryCollection = Database.GetCollection<PointQuery>("PointCollection");
            RequestTargetQueryCollection = Database.GetCollection<RequestTargetQuery>("RequestTargetCollection");
            RequestOrgQueryCollection = Database.GetCollection<RequestOrgQuery>("RequestOrgCollection");
            RequestActQueryCollection = Database.GetCollection<RequestActQuery>("RequestActCollection");
            // RequestQueryCollection = Database.GetCollection<RequestQuery>("RequestCollection");
            SubjectQueryCollection = Database.GetCollection<SubjectQuery>("SubjectCollection");
            // AnnouncementQueryCollection = Database.GetCollection<AnnouncementQuery>("AnnouncementCollection");
        }

        public void RemoveDB()
        {
            Database.Client.DropDatabase(Settings.DatabaseName);
        }

        public IReadModelDatabaseSettings Settings { get; set; }
        public IMongoCollection<LabelQuery> LabelQueryCollection { get; set; }
        public IMongoCollection<PointQuery> PointQueryCollection { get; set; }
        public IMongoCollection<RequestTargetQuery> RequestTargetQueryCollection { get; set; }
        public IMongoCollection<RequestOrgQuery> RequestOrgQueryCollection { get; set; }
        public IMongoCollection<RequestActQuery> RequestActQueryCollection { get; set; }
        // public IMongoCollection<RequestQuery> RequestQueryCollection { get; set; }
        public IMongoCollection<SubjectQuery> SubjectQueryCollection { get; set; }
        // public IMongoCollection<AnnouncementQuery> AnnouncementQueryCollection { get; set; }
        // public IMongoCollection<TechnicianQuery> TechnicianQueryCollection { get; set; }

        public IMongoDatabase Database { get; set; }
    }
}