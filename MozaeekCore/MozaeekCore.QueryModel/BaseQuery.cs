using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MozaeekCore.QueryModel
{
    public class BaseQuery
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime LastEventPublishDate { get; set; }
        public Guid LastEventId { get; set; }
    }
}