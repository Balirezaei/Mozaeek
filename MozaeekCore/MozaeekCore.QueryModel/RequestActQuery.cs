using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MozaeekCore.QueryModel
{
    public class RequestActQuery :BaseQuery
    {
        public RequestActQuery(long id, string title, DateTime lastEventPublishDate, Guid lastEventId)
        {
            Id = id;
            Title = title;
            LastEventPublishDate = lastEventPublishDate;
            LastEventId = lastEventId;
        }
        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        public long Id { get; set; }
        public string Title { get; set; } }
}