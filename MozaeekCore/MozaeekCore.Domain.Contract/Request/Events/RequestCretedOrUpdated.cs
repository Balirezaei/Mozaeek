using System;
using System.Collections.Generic;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract.Request.Events
{
    public class RequestCretedOrUpdated : IEvent
    {
        public long Id { get; set; }
        public long RequestTargetId { get; private set; }
        public long RequestActId { get; private set; }

        public List<RequestDependency> RequestActions { get; private set; }
        public List<RequestDependency> RequestNecessities { get; private set; }
        //public List<RequestDependency> RequestDocuments { get; private set; }
        public List<RequestDependency> RequestQualifications { get; private set; }
        public List<RequestTargetConnectionEventDto> RequestTargetConnectionEventDtos { get;private set; }
        public List<long> PointIds { get; set; }
        public List<long> RequestOrgs { get; set; }
        public List<long> DefiniteRequestOrgs { get; set; }

        public DateTime? RequestExpiredDate { get; set; }
        public DateTime? RequestStartDate { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
        public bool IsCreated { get; set; }
        public bool FullOnline { get; set; }
        public string Description { get; set; }
        public string Regulation { get; private set; }
        public RequestCretedOrUpdated(long id, long requestTargetId, 
            long requestActId, List<RequestDependency> requestActions, 
            List<RequestDependency> requestNecessities, List<RequestDependency> requestQualifications, 
            List<long> pointIds, DateTime? requestExpiredDate, DateTime? requestStartDate,
            bool fullOnline, string description, string regulation, List<RequestTargetConnectionEventDto> requestTargetConnectionEventDtos,
            List<long> requestOrgs, List<long> definiteRequestOrgs, bool isCreated)
        {
            Id = id;
            RequestTargetId = requestTargetId;
            RequestActId = requestActId;
            RequestActions = requestActions;
            RequestNecessities = requestNecessities;
            RequestOrgs = requestOrgs;
            DefiniteRequestOrgs = definiteRequestOrgs;
            RequestQualifications = requestQualifications;
            RequestExpiredDate = requestExpiredDate;
            RequestStartDate = requestStartDate;
            IsCreated = isCreated;
            RequestTargetConnectionEventDtos = requestTargetConnectionEventDtos;
            Regulation = regulation;
            Description = description;
            FullOnline = fullOnline;
            PointIds = pointIds;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }

        protected RequestCretedOrUpdated()
        {
        }
    }
}