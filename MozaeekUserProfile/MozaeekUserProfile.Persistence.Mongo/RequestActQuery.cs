using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MozaeekUserProfile.Persistence.Mongo
{
    public class RequestActQuery
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime LastEventPublishDate { get; set; }
        public Guid LastEventId { get; set; }
    }
}