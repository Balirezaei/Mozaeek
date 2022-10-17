using System.Collections.Generic;
using MozaeekCore.Common.ExtensionMethod;

namespace MozaeekCore.Domain
{
    /// <summary>
    /// کارمراد
    /// کارت ملی/کارت بازرگانی/گواهینامه
    /// </summary>
    public class RequestAct : BasicInfo
    {
        public string Title { get; private set; }
        public virtual ICollection<Request> Requests { get; private set; }
        public RequestAct(long id,string title)
        {
            Id = id;
            Title = title.Recheck();
        }

        public void UpdateTitle(string title)
        {
            this.Title = title.Recheck();
        }
    }
}