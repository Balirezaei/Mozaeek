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
        public List<RequestDependency> RequestNessesities { get; private set; }
        public List<RequestDependency> RequestDocuments { get; private set; }
        public List<RequestDependency> RequestQualifications { get; private set; }
        public List<long> PointIds { get; set; }

        public DateTime? RequestExpiredDate { get; set; }
        public DateTime? RequestStartDate { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
        public bool IsCreated { get; set; }

        public RequestCretedOrUpdated(long id, long requestTargetId, long requestActId, List<RequestDependency> requestActions, List<RequestDependency> requestNessesities, List<RequestDependency> requestDocuments, List<RequestDependency> requestQualifications, List<long> pointIds, DateTime? requestExpiredDate, DateTime? requestStartDate, bool isCreated)
        {
            Id = id;
            RequestTargetId = requestTargetId;
            RequestActId = requestActId;
            RequestActions = requestActions;
            RequestNessesities = requestNessesities;
            RequestDocuments = requestDocuments;
            RequestQualifications = requestQualifications;
            RequestExpiredDate = requestExpiredDate;
            RequestStartDate = requestStartDate;
            IsCreated = isCreated;
            PointIds = pointIds;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }

        protected RequestCretedOrUpdated(bool isCreated, List<long> pointIds)
        {
            IsCreated = isCreated;
            PointIds = pointIds;
        }
    }
}