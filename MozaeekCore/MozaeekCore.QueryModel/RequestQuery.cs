using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MozaeekCore.QueryModel
{
    public class RequestQuery : BaseQuery
    {
        public RequestQuery(long id, string title,
            RequestActQuery requestAct,
            RequestTargetQuery requestTarget,
            //List<RequestQueryDependency> documents,
            List<RequestQueryDependency> necessities,
            List<RequestQueryDependency> actions,
            List<RequestQueryDependency> qualifications,
            List<PointQuery> poins,
            List<RequestOrgQuery> requestOrgs,
            List<DefiniteRequestOrgQuery> definiteRequestOrg,
            //List<PointQuery> pointsChildren,
            List<ConnectedRequestTarget> connectedRequestTagets,
            DateTime lastEventPublishDate, Guid lastEventId, bool fullOnline, string description, string regulation, DateTime? requestExpiredDate, DateTime? requestStartDate)
        {
            Id = id;
            Title = title;
            RequestAct = requestAct;
            RequestTarget = requestTarget;
            //Documents = documents;
            Necessities = necessities;
            Actions = actions;
            Qualifications = qualifications;
            Points = poins;
            RequestOrgs = requestOrgs;
            DefiniteRequestOrgs = definiteRequestOrg;
            ConnectedRequestTargets = connectedRequestTagets;
            LastEventPublishDate = lastEventPublishDate;
            LastEventId = lastEventId;
            FullOnline = fullOnline;
            Description = description;
            Regulation = regulation;
            RequestExpiredDate = requestExpiredDate;
            RequestStartDate = requestStartDate;
        }

        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        public long Id { get; set; }
        public string Title { get; set; }
        public RequestActQuery RequestAct { get; set; }
        public RequestTargetQuery RequestTarget { get; set; }
        //public List<RequestQueryDependency> Documents { get; set; }
        public List<RequestQueryDependency> Necessities { get; set; }
        public List<RequestQueryDependency> Actions { get; set; }
        public List<RequestQueryDependency> Qualifications { get; set; }
        public List<PointQuery> Points { get; set; }

        public List<RequestOrgQuery> RequestOrgs { get; set; }

        public List<DefiniteRequestOrgQuery> DefiniteRequestOrgs { get; set; }
        //public IList<PointQuery> PointsChildren { get; set; }
        public List<ConnectedRequestTarget> ConnectedRequestTargets { get; set; }
        public bool FullOnline { get; set; }
        public string Description { get; set; }
        public string Regulation { get; set; }

        /// <summary>
        /// تاریخ پایان خواست
        /// </summary>
        public DateTime? RequestExpiredDate { get; set; }

        /// <summary>
        /// تاریخ شروع خواست
        /// </summary>
        public DateTime? RequestStartDate { get; set; }
    }

    public class ConnectedRequestTarget
    {
        public ConnectedRequestTarget(long requestTargetId, string description)
        {
            RequestTargetId = requestTargetId;
            Description = description;
        }

        public string Title { get; set; }
        public long RequestTargetId { get; set; }
        public string Description { get; set; }

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
        public RequestParameter(long id, long targetId, long actId, 
            List<RequestQueryDependency> necessities,
            List<RequestQueryDependency> actions, List<RequestQueryDependency> qualificationIds,
            List<long> pointIds, bool fullOnline, string summary, string regulation, DateTime? requestExpiredDate, DateTime? requestStartDate, List<ConnectedRequestTarget> connectedRequestTargets)
        {
            Id = id;
            TargetId = targetId;
            ActId = actId;
            //Documents = documents;
            Necessities = necessities;
            Actions = actions;
            QualificationIds = qualificationIds;
            PointIds = pointIds;
            FullOnline = fullOnline;
            Summary = summary;
            Regulation = regulation;
            RequestExpiredDate = requestExpiredDate;
            RequestStartDate = requestStartDate;
            ConnectedRequestTargets = connectedRequestTargets;
        }

        public RequestParameter()
        {
        }

        public long Id { get; set; }
        public long TargetId { get; set; }
        public long ActId { get; set; }
        //public List<RequestQueryDependency> Documents { get; set; }
        public List<RequestQueryDependency> Necessities { get; set; }
        public List<RequestQueryDependency> Actions { get; set; }
        public List<RequestQueryDependency> QualificationIds { get; set; }
        public List<long> PointIds { get; set; }
        public List<long> RequestOrgs { get; set; }
        public List<long> DefiniteRequestOrgs { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishEventDate { get; set; }
        public bool FullOnline { get; set; }
        public string Summary { get; set; }
        public string Regulation { get; set; }
        public DateTime? RequestExpiredDate { get; set; }

        public DateTime? RequestStartDate { get; set; }
        public List<ConnectedRequestTarget> ConnectedRequestTargets { get; set; }

    }
}