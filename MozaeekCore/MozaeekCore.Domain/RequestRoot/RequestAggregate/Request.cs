using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Core.Domain;
using MozaeekCore.Exceptions;

namespace MozaeekCore.Domain
{
    public class Request : AggregateRootBase
    {
        protected Request()
        {
        }
        public long Id { get; set; }
        /// <summary>
        /// شرح
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// مقررات مربوطه
        /// </summary>
        public string Regulation { get; private set; }
        public long RequestTargetId { get; private set; }
        public RequestTarget RequestTarget { get; private set; }

        public long RequestActId { get; private set; }
        public RequestAct RequestAct { get; private set; }

        public virtual ICollection<RequestAction> RequestActions { get; private set; }

        public virtual ICollection<RequestNecessity> RequestNecessities { get; private set; }

        //public virtual ICollection<RequestDocument> RequestDocuments { get; private set; }

        public virtual ICollection<RequestQualification> RequestQualifications { get; private set; }

        public virtual ICollection<RequestPoint> RequestPoints { get; private set; }
        public virtual ICollection<RequestDefiniteRequestOrg> RequestDefiniteRequestOrgs { get; private set; }
        public virtual ICollection<RequestRequestOrg> RequestRequestOrgs { get; private set; }

        public virtual ICollection<RequestTargetConnection> RequestTargetConnections { get; private set; }
        public virtual ICollection<Announcement> Announcements { get; private set; }
        public bool FullOnline { get; set; }

        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// تاریخ پایان خواست
        /// </summary>
        public DateTime? RequestExpiredDate { get; private set; }

        /// <summary>
        /// تاریخ شروع خواست
        /// </summary>
        public DateTime? RequestStartDate { get; private set; }



        public Request(long requestTargetId, long requestActId, RequestDomainInput requestInput,
            RequestDependencyInput dependency,
             List<RequestPoint> requestPoints,
             List<RequestTargetConnection> requestTargetConnection,
            DateTime? requestExpiredDate, DateTime? requestStartDate, string description, string regulation)
        {
            RequestTargetId = requestTargetId;
            RequestActId = requestActId;
            RequestActions = dependency.RequestActions;
            RequestNecessities = dependency.RequestNecessities;
            //RequestDocuments = requestDocuments;
            RequestQualifications = dependency.RequestQualifications;
            RequestPoints = requestPoints;
            RequestExpiredDate = requestExpiredDate;
            RequestStartDate = requestStartDate;
            RequestTargetConnections = requestTargetConnection;
            Regulation = regulation.Recheck();
            Description = description.Recheck();
            FullOnline = requestInput.FullOnline;
            if (FullOnline)
            {
                var onlineRequestInput = (OnlineRequestInput)requestInput;
                if (!onlineRequestInput.RequestRequestOrgs.Any())
                {
                    throw new Exception("انتخاب خواستگاه پایه اجباریست.");
                }
                RequestRequestOrgs = onlineRequestInput.RequestRequestOrgs;
            }
            else
            {
                var offlineRequestInput = (OfflineRequestInput)requestInput;
                if (!offlineRequestInput.RequestDefiniteRequestOrgs.Any())
                {
                    throw new Exception("انتخاب خواستگاه معین اجباریست.");
                }
                RequestDefiniteRequestOrgs = offlineRequestInput.RequestDefiniteRequestOrgs;
            }
            CreateDate = DateTime.Now;
        }
  
        public void Update(long requestTargetId, long requestActId, RequestDomainInput requestInput, DateTime? requestExpiredDate,
            DateTime? requestStartDate, string description, string regulation)
        {
            RequestTargetId = requestTargetId;
            RequestActId = requestActId;
            FullOnline = false;
            FullOnline = requestInput.FullOnline;
            if (FullOnline)
            {
                var onlineRequestInput = (OnlineRequestInput)requestInput;
                if (!onlineRequestInput.RequestRequestOrgs.Any())
                {
                    throw new Exception("انتخاب خواستگاه پایه اجباریست.");
                }
                RequestRequestOrgs = onlineRequestInput.RequestRequestOrgs;
            }
            else
            {
                var offlineRequestInput = (OfflineRequestInput)requestInput;
                if (!offlineRequestInput.RequestDefiniteRequestOrgs.Any())
                {
                    throw new Exception("انتخاب خواستگاه معین اجباریست.");
                }
                RequestDefiniteRequestOrgs = offlineRequestInput.RequestDefiniteRequestOrgs;
            }
            RequestExpiredDate = requestExpiredDate;
            RequestStartDate = requestStartDate;
            Description = description.Recheck();
            Regulation = regulation.Recheck();
        }

       public void ResetAssociations()
        {
            this.RequestPoints.Clear();
            this.RequestActions.Clear();
            //this.RequestDocuments.Clear();
            this.RequestQualifications.Clear();
            this.RequestNecessities.Clear();
            this.RequestTargetConnections.Clear();
            this.RequestDefiniteRequestOrgs.Clear();
            this.RequestRequestOrgs.Clear();
        }
        public void UpdateAssociation(List<RequestAction> requestActions, List<RequestNecessity> requestNecessities,
            //  List<RequestDocument> requestDocuments,
            List<RequestQualification> requestQualifications, List<RequestTargetConnection> requestTargetConnection,
            List<RequestPoint> requestPoints)
        {
            RequestActions = requestActions;
            RequestNecessities = requestNecessities;
            //RequestDocuments = requestDocuments;
            RequestQualifications = requestQualifications;
            RequestTargetConnections = requestTargetConnection;
            RequestPoints = requestPoints;
        }

    }

    public class RequestDomainInput
    {
        public bool FullOnline { get; set; }
    }
    public class RequestDependencyInput
    {
        public virtual ICollection<RequestAction> RequestActions { get; private set; }
        public virtual ICollection<RequestNecessity> RequestNecessities { get; private set; }

        //public virtual ICollection<RequestDocument> RequestDocuments { get; private set; }
        public virtual ICollection<RequestQualification> RequestQualifications { get; private set; }
        public RequestDependencyInput(ICollection<RequestAction> requestActions, ICollection<RequestNecessity> requestNecessities, ICollection<RequestQualification> requestQualifications)
        {
            RequestActions = requestActions;
            RequestNecessities = requestNecessities;
            RequestQualifications = requestQualifications;

        }
    }

    public class OnlineRequestInput : RequestDomainInput
    {
        public virtual ICollection<RequestRequestOrg> RequestRequestOrgs { get; private set; }

        public OnlineRequestInput(ICollection<RequestRequestOrg> requestRequestOrgs)
        {
            RequestRequestOrgs = requestRequestOrgs;
            FullOnline = true;
        }
    }
    public class OfflineRequestInput : RequestDomainInput
    {
        public virtual ICollection<RequestDefiniteRequestOrg> RequestDefiniteRequestOrgs { get; private set; }

        public OfflineRequestInput(ICollection<RequestDefiniteRequestOrg> requestDefiniteRequestOrgs)
        {
            RequestDefiniteRequestOrgs = requestDefiniteRequestOrgs;
            FullOnline = false;
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

    public class RequestDefiniteRequestOrg
    {
        public long Id { get; set; }
        public long DefiniteRequestOrgId { get; set; }
        public virtual DefiniteRequestOrg DefiniteRequestOrg { get; set; }

        public long RequestId { get; set; }
        public virtual Request Request { get; set; }
    }

    public class RequestRequestOrg
    {
        public long Id { get; set; }
        public long RequestOrgId { get; set; }
        public virtual RequestOrg RequestOrg { get; set; }

        public long RequestId { get; set; }
        public virtual Request Request { get; set; }
    }

}