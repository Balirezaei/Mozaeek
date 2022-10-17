using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MozaeekTechnicianProfile.Persistence.Mongo
{
    public class RequestOrgQuery
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        public long Id { get; set; }
        public string Title { get; set; }
        public bool HasChild { get; set; }
        public long? ParentId { get; set; }
        public DateTime LastEventPublishDate { get; set; }
        public Guid LastEventId { get; set; }

    }
}
