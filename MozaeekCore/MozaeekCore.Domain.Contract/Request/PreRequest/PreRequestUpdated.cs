using System;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract.BasicInfo
{
    public class PreRequestUpdated : IEvent
    {
        public PreRequestUpdated(long id,  string title, string summery, bool isProcessed)
        {
            Id = id;
            Title = title;
            Summery = summery;
            IsProcessed = isProcessed;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }

        public long Id { get; private set; }
        public string Title { get; private set; }
        public string Summery { get; private set; }
        public bool IsProcessed { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
        protected PreRequestUpdated(bool isProcessed)
        {
            IsProcessed = isProcessed;
        }
    }
}