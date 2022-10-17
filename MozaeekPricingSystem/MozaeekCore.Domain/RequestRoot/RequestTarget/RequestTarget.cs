using System.Collections.Generic;
using MozaeekCore.Core.Domain;

namespace MozaeekCore.Domain
{
    /// <summary>
    /// کارواژه
    /// اخذ/تمدید/تعویض/
    /// </summary>
    public class RequestTarget : AggregateRootBase
    {
        public long Id { get; set; }
        public string Title { get; private set; }
        public virtual ICollection<RequestTargetSubject> RequestTargetSubjects { get; private set; }
        public virtual ICollection<RequestTargetLabel> RequestTargetLabels { get; private set; }
        public virtual ICollection<RequestTargetRequestOrg> RequestTargetRequestOrgs { get; private set; }
        public virtual ICollection<Request> Requests { get; private set; }
        public virtual ICollection<Announcement> Announcements { get; set; }
        protected RequestTarget()
        {

        }

        public RequestTarget(long id, string title, List<RequestTargetSubject> requestTargetSubjects, List<RequestTargetLabel> requestTargetLabels, List<RequestTargetRequestOrg> requestTargetRequestOrgs)
        {
            Id = id;
            Title = title;
            RequestTargetSubjects = requestTargetSubjects;
            RequestTargetLabels = requestTargetLabels;
            RequestTargetRequestOrgs = requestTargetRequestOrgs;
        }

        public void UpdateAssociations(List<RequestTargetSubject> requestTargetSubjects,
            List<RequestTargetLabel> requestTargetLabels, List<RequestTargetRequestOrg> requestTargetRequestOrgs)
        {
            RequestTargetSubjects = requestTargetSubjects;
            RequestTargetLabels = requestTargetLabels;
            RequestTargetRequestOrgs = requestTargetRequestOrgs;
        }
    }
}