using System.Collections.Generic;
using MozaeekCore.Common.ExtensionMethod;

namespace MozaeekCore.Domain
{
    /// <summary>
    /// خواستگاه
    /// وزارت صنعت و معدن
    /// </summary>
    public class RequestOrg : BasicInfo
    {
        public string Title { get; private set; }
        public string Icon { get; set; }
        public long? ParentId { get; private set; }
        public virtual RequestOrg Parent { get; private set; }
        public virtual ICollection<RequestOrg> SubRequestOrgs { get; } = new List<RequestOrg>();
        //public virtual ICollection<RequestTargetRequestOrg> RequestTargetRequestOrgs { get; set; }
        public virtual ICollection<AnnouncementRequestOrg> AnnouncementRequestOrgs { get; set; }
        public virtual ICollection<DefiniteRequestOrg> DefiniteRequestOrgs { get; set; }
        public virtual ICollection<RequestRequestOrg> RequestRequestOrgs { get; set; }
        public RequestOrg(long id, string title, string icon, long? parentId = null)
        {
            Id = id;
            Title = title.Recheck();
            ParentId = parentId;
            Icon = icon;
        }

        public RequestOrg(long id, string title, List<RequestOrg> children)
        {
            Id = id;
            Title = title.Recheck();
            SubRequestOrgs = children;
        }

        public void Update(string title, string icon)
        {
            this.Title = title.Recheck();
            Icon = icon;
        }
    }
}