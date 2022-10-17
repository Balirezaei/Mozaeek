using System.Collections.Generic;
using MozaeekCore.Common.ExtensionMethod;

namespace MozaeekCore.Domain
{
    /// <summary>
    ///زمینه
    /// کسب و کار/امور بازرگانی
    /// </summary>
    public class Subject : BasicInfo
    {
        public string Title { get; private set; }
        public long? ParentId { get; private set; }
        public virtual Subject Parent { get; private set; }
        public ICollection<Subject> SubSubjects { get; } = new List<Subject>();
        public virtual ICollection<RequestTargetSubject> RequestTargetSubjects { get; set; }
        public virtual ICollection<AnnouncementSubject> AnnouncementSubjects { get; set; }
        public string Icon { get; private set; }
        public Subject(long id, string title, string icon, long? parentId = null)
        {
            Id = id;
            Title = title.Recheck();
            ParentId = parentId;
            Icon = icon;
        }
        public Subject(long id, string title, List<Subject> children)
        {
            Id = id;
            Title = title.Recheck();
            SubSubjects = children;
        }
        public void Update(string title,string icon)
        {
            Title = title.Recheck();
            Icon = icon;
        }
    }
}