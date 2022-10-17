using System.Collections.Generic;
using MozaeekCore.Common.ExtensionMethod;
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
        public string Icon { get; private set; }

        public virtual ICollection<RequestTargetSubject> RequestTargetSubjects { get; private set; }
        public virtual ICollection<RequestTargetLabel> RequestTargetLabels { get; private set; }
        //public virtual ICollection<RequestTargetRequestOrg> RequestTargetRequestOrgs { get; private set; }
        public virtual ICollection<Request> Requests { get; private set; }
        /// <summary>
        /// آیا نشان برگه است؟
        /// سند چاپی مثل گواهینامه
        /// 
        /// </summary>
        public bool IsDocument { get; private set; }

        protected RequestTarget()
        {
        }

        public RequestTarget(long id, string title, List<RequestTargetSubject> requestTargetSubjects, List<RequestTargetLabel> requestTargetLabels, bool isDocument, string icon)
        {
            Id = id;
            Title = title.Recheck();
            RequestTargetSubjects = requestTargetSubjects;
            RequestTargetLabels = requestTargetLabels;
            //RequestTargetRequestOrgs = requestTargetRequestOrgs;
            IsDocument = isDocument;
            Icon = icon;
        }

        public void Update(string title, List<RequestTargetSubject> requestTargetSubjects,
            List<RequestTargetLabel> requestTargetLabels, bool isDocument,string icon)
        {
            Title = title.Recheck();
            RequestTargetSubjects = requestTargetSubjects;
            RequestTargetLabels = requestTargetLabels;
            //RequestTargetRequestOrgs = requestTargetRequestOrgs;
            IsDocument = isDocument;
            Icon = icon;
        }
    }
}