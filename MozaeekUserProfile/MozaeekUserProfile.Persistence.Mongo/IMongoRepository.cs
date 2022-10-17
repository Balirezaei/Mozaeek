using MongoDB.Driver;

namespace MozaeekUserProfile.Persistence.Mongo
{
    public interface IMongoRepository
    {
        IReadModelDatabaseSettings Settings { get; set; }
        IMongoDatabase Database { get; set; }
        IMongoCollection<LabelQuery> LabelQueryCollection { get; set; }
        IMongoCollection<PointQuery> PointQueryCollection { get; set; }
        IMongoCollection<RequestTargetQuery> RequestTargetQueryCollection { get; set; }
        IMongoCollection<RequestOrgQuery> RequestOrgQueryCollection { get; set; }
        IMongoCollection<RequestActQuery> RequestActQueryCollection { get; set; }
        // IMongoCollection<RequestQuery> RequestQueryCollection { get; set; }
        IMongoCollection<SubjectQuery> SubjectQueryCollection { get; set; }
        // IMongoCollection<AnnouncementQuery> AnnouncementQueryCollection { get; set; }
        // IMongoCollection<TechnicianQuery> TechnicianQueryCollection { get; set; }
    }
}