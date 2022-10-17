using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MozaeekCore.QueryModel
{
    public class SubjectQuery : BaseQuery
    {
        public SubjectQuery(long id, string title, string icon, long? parentId, DateTime lastEventPublishDate, Guid lastEventId)
        {
            Id = id;
            Title = title;
            ParentId = parentId;
            Icon = icon;
            LastEventPublishDate = lastEventPublishDate;
            LastEventId = lastEventId;
        }

        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public bool HasChild { get; set; }
        public long? ParentId { get; set; }

    }
}