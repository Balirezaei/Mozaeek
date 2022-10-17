using System;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Core.Domain;

namespace MozaeekCore.Domain
{
    public class PreRequest : AggregateRootBase
    {
        public DateTime CreateDateTime { get; private set; }
        public long Id { get; private set; }
        public string Title { get; private set; }
        public string Summary { get; private set; }
        public bool IsProcessed { get; private set; }
        protected PreRequest() { }
        public PreRequest(string title, string summery)
        {
            Title = title.Recheck();
            Summary = summery.Recheck();
            this.CreateDateTime = DateTime.Now;
            this.IsProcessed = false;
        }

        public void UpdateTitle(string title, string summery)
        {
            Title = title.Recheck();
            Summary = summery.Recheck();
        }

        public void ChangeToProcessed()
        {
            IsProcessed = true;
        }
    }
}