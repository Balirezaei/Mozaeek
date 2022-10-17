using System.Collections.Generic;
using MozaeekCore.Common.ExtensionMethod;

namespace MozaeekCore.Domain
{
    /// <summary>
    /// نقاط
    /// خراسان رضوی/کاشمر/ریوش
    /// </summary>
    public class Point : BasicInfo
    {
        public string Title { get; private set; }
        public long? ParentId { get; private set; }
        public virtual Point Parent { get; private set; }
        public ICollection<Point> SubPoints { get; } = new List<Point>();
        public virtual ICollection<AnnouncementPoint> AnnouncementPoints { get; set; }
        public virtual ICollection<RequestPoint> RequestPoints { get; set; }
        public Point(long id, string title, long? parentId = null)
        {
            Id = id;
            Title = title.Recheck();
            ParentId = parentId;
        }
        public Point(long id, string title, List<Point> children)
        {
            Id = id;
            Title = title.Recheck();
            SubPoints = children;
        }
        public void UpdateTitle(string title)
        {
            Title = title.Recheck();
        }
    }
}