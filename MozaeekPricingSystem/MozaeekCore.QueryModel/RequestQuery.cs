using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MozaeekCore.QueryModel
{
    public class RequestQuery
    {
        public RequestQuery(long id, string title,
            RequestActQuery requestAct,
            RequestTargetQuery requestTarget,
            List<RequestQueryDependency> documents,
            List<RequestQueryDependency> nessesities,
            List<RequestQueryDependency> actions,
            List<RequestQueryDependency> qualifications,
            List<PointQuery> poins,
            DateTime lastEventPublishDate, Guid lastEventId)
        {
            Id = id;
            Title = title;
            RequestAct = requestAct;
            RequestTarget = requestTarget;
            Documents = documents;
            Nessesities = nessesities;
            Actions = actions;
            Qualifications = qualifications;
            Points = poins;
            LastEventPublishDate = lastEventPublishDate;
            LastEventId = lastEventId;
        }

        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        public long Id { get; set; }
        public string Title { get; set; }
        public RequestActQuery RequestAct { get; set; }
        public RequestTargetQuery RequestTarget { get; set; }
        public List<RequestQueryDependency> Documents { get; set; }
        public List<RequestQueryDependency> Nessesities { get; set; }
        public List<RequestQueryDependency> Actions { get; set; }
        public List<RequestQueryDependency> Qualifications { get; set; }
        public List<PointQuery> Points { get; set; }
        public DateTime LastEventPublishDate { get; set; }
        public Guid LastEventId { get; set; }
        public RequestQuery Query { get; set; }
    }

    public class RequestQueryDependency
    {
        public RequestQueryDependency(string description, int priority)
        {
            Description = description;
            Priority = priority;
        }

        public long Id { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }

    public class RequestParameter
    {
        public RequestParameter(long id, long targetId, long actId, List<RequestQueryDependency> documents, List<RequestQueryDependency> nessesities,
            List<RequestQueryDependency> actions, List<RequestQueryDependency> qualificationIds, List<long> pointIds)
        {
            Id = id;
            TargetId = targetId;
            ActId = actId;
            Documents = documents;
            Nessesities = nessesities;
            Actions = actions;
            QualificationIds = qualificationIds;
            PointIds = pointIds;
        }

        public long Id { get; set; }
        public long TargetId { get; set; }
        public long ActId { get; set; }
        public List<RequestQueryDependency> Documents { get; set; }
        public List<RequestQueryDependency> Nessesities { get; set; }
        public List<RequestQueryDependency> Actions { get; set; }
        public List<RequestQueryDependency> QualificationIds { get; set; }
        public List<long> PointIds { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishEventDate { get; set; }
    }
}