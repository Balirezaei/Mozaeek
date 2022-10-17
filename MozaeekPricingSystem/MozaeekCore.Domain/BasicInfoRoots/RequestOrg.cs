using System.Collections.Generic;
using MozaeekCore.Common.ExtentionMethod;

namespace MozaeekCore.Domain
{
    /// <summary>
    /// خواستگاه
    /// وزارت صنعت و معدن
    /// </summary>
    public class RequestOrg : BasicInfo
    {
        public string Title { get; private set; }
        public long? ParentId { get; private set; }
        public virtual RequestOrg Parent { get; private set; }
        public virtual ICollection<RequestOrg> SubRequestOrgs { get; } = new List<RequestOrg>();
        public virtual ICollection<RequestTargetRequestOrg> RequestTargetRequestOrgs { get; set; }

        public RequestOrg(long id, string title, long? parentId = null)
        {
            Id = id;
            Title = title.Recheck();
            ParentId = parentId;
        }

        public void UpdateTitle(string title)
        {
            this.Title = title.Recheck();
        }
    }
}