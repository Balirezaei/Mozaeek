using System.Collections.Generic;
using MozaeekCore.Common.ExtensionMethod;

namespace MozaeekCore.Domain
{
    /// <summary>
    ///برچسب
    /// تحصیلات/دانشگاهی/کارشناسی
    /// وضعیت شغلی/شاغل/آزاد
    /// </summary>
    public class Label : BasicInfo
    {
        public string Title { get; private set; }
        public long? ParentId { get; private set; }
        public virtual Label Parent { get; private set; }
        public virtual ICollection<Label> SubLabels { get; } = new List<Label>();
        public virtual ICollection<RequestTargetLabel> RequestTargetLabels { get; set; }
        public virtual ICollection<AnnouncementLabel> AnnouncementLabels { get; set; }

        public Label(long id, string title, long? parentId = null)
        {
            Id = id;
            Title = title.Recheck();
            ParentId = parentId;
        }
        public Label(long id, string title, List<Label> children)
        {
            Id = id;
            Title = title.Recheck();
            SubLabels = children;
        }
        public void UpdateTitle(string newTitle)
        {
            this.Title = newTitle.Recheck();
        }
    }
}