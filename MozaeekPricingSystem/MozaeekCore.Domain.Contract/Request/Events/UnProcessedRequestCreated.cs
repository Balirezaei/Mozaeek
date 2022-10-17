using System;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract.Request.Events
{
    public class UnProcessedRequestCreated : IEvent
    {
        public long Id { get; private set; }
        public string Title { get; private set; }
        public string Summery { get; private set; }
        public bool IsProcessed { get; private set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
        public UnProcessedRequestCreated(long id, string title, string summery, bool isProcessed)
        {
            Id = id;
            Title = title;
            Summery = summery;
            IsProcessed = isProcessed;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }

        public UnProcessedRequestCreated(long id, string title, string summery, bool isProcessed, Guid eventId, DateTime publishDateTime)
        {
            Id = id;
            Title = title;
            Summery = summery;
            IsProcessed = isProcessed;
            EventId = eventId;
            PublishDateTime = publishDateTime;
        }
    }
}
