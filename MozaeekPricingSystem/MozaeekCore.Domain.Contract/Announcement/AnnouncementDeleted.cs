using System;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract
{
    public class AnnouncementDeleted : IEvent
    {
        public long Id { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }

        public AnnouncementDeleted(long id)
        {
            Id = id;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }

        protected AnnouncementDeleted() { }
    }
}