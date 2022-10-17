using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MozaeekCore.QueryModel
{
    public class LabelQuery : BaseQuery
    {
        public LabelQuery(long id, string title, long? parentId, DateTime lastEventPublishDate, Guid lastEventId)
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

        // public List<LabelQueryLinked> Parents { get; set; }
        
        public bool HasChild { get; set; }
        public long? ParentId { get; set; }

    }

    // public class LabelQueryLinked
    // {
    //     public long Id { get; set; }
    //     public string Title { get; set; }
    //     public long? ParentId { get; set; }
    // }
}
