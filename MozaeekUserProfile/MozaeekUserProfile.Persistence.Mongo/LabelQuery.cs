using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MozaeekUserProfile.Persistence.Mongo
{
    public class LabelQuery
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        public long Id { get; set; }
        public string Title { get; set; }

        // public List<LabelQueryLinked> Parents { get; set; }

        public bool HasChild { get; set; }
        public long? ParentId { get; set; }
        public DateTime LastEventPublishDate { get; set; }
        public Guid LastEventId { get; set; }
    }
}