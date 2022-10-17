using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Syncfusion.Drawing;

namespace MozaeekCore.QueryModel
{
    public class RequestOrgQuery : BaseQuery
    {
        public RequestOrgQuery(long id, string title, string icon, long? parentId, DateTime lastEventPublishDate, Guid lastEventId)
        {
            Id = id;
            Title = title;
            Icon = icon;
            ParentId = parentId;
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