using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MozaeekCore.QueryModel
{
    public class SubjectQuery
    {
        public SubjectQuery(long id, string title, long? parentId, DateTime lastEventPublishDate, Guid lastEventId)
        {
            Id = id;
            Title = title;
            ParentId = parentId;
            LastEventPublishDate = lastEventPublishDate;
            LastEventId = lastEventId;
        }

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