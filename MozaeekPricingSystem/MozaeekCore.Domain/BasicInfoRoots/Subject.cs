using System.Collections.Generic;
using MozaeekCore.Common.ExtentionMethod;

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
        public virtual  ICollection<RequestTargetSubject> RequestTargetSubjects { get; set; }

        public Subject(long id, string title, long? parentId = null)
        {
            Id = id;
            Title = title.Recheck();
            ParentId = parentId;
        }

        public void UpdateTitle(string title)
        {
            Title = title.Recheck();
        }
    }
}