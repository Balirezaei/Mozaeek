using System;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract.BasicInfo
{
    public class RequestActCretedOrUpdated : IEvent
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool IsCreated { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }

        public RequestActCretedOrUpdated(long id, string title, bool isCreated)
        {
            Id = id;
            Title = title;
            IsCreated = isCreated;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }

        protected RequestActCretedOrUpdated() { }
    }
}