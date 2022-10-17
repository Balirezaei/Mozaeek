using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MozaeekCore.Core.Domain;

namespace MozaeekCore.Domain
{
    public class Request : AggregateRootBase
    {
        protected Request() { }
        public long Id { get; set; }
        public long RequestTargetId { get; private set; }
        public RequestTarget RequestTarget { get; private set; }

        public long RequestActId { get; private set; }
        public RequestAct RequestAct { get; private set; }

        public virtual ICollection<RequestAction> RequestActions { get; private set; }

        public virtual ICollection<RequestNessesity> RequestNessesities { get; private set; }

        public virtual ICollection<RequestDocument> RequestDocuments { get; private set; }

        public virtual ICollection<RequestQualification> RequestQualifications { get; private set; }

        public virtual ICollection<RequestPoint> RequestPoints { get; private set; }

        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// تاریخ پایان خواست
        /// </summary>
        public DateTime? RequestExpiredDate { get; private set; }

        /// <summary>
        /// تاریخ شروع خواست
        /// </summary>
        public DateTime? RequestStartDate { get; private set; }



        public Request(long requestTargetId, long requestActId,
            List<RequestAction> requestActions, List<RequestNessesity> requestNessesities,
            List<RequestDocument> requestDocuments, List<RequestQualification> requestQualifications,
             List<RequestPoint> requestPoints,
            DateTime? requestExpiredDate, DateTime? requestStartDate)
        {
            RequestTargetId = requestTargetId;
            RequestActId = requestActId;
            RequestActions = requestActions;
            RequestNessesities = requestNessesities;
            RequestDocuments = requestDocuments;
            RequestQualifications = requestQualifications;
            RequestPoints = requestPoints;
            RequestExpiredDate = requestExpiredDate;
            RequestStartDate = requestStartDate;
            CreateDate = DateTime.Now;
        }

        public void ResetAssociations()
        {
            this.RequestPoints.Clear();
            this.RequestActions.Clear();
            this.RequestDocuments.Clear();
            this.RequestQualifications.Clear();
            this.RequestNessesities.Clear();
        }
        public void UpdateAssociation(List<RequestAction> requestActions, List<RequestNessesity> requestNessesities,
            List<RequestDocument> requestDocuments, List<RequestQualification> requestQualifications,
            List<RequestPoint> requestPoints)
        {
            RequestActions = requestActions;
            RequestNessesities = requestNessesities;
            RequestDocuments = requestDocuments;
            RequestQualifications = requestQualifications;
            RequestPoints = requestPoints;
        }
    }

    public class RequestPoint
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public virtual Request Request { get; set; }
        public long PointId { get; set; }
        public virtual Point Point { get; set; }
    }
}