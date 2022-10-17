using System;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract.Request.Events
{
    public class AnnouncementAssignRequestEvent : IEvent
    {
        public long RequestId { get; set; }
        public long AnnouncementId { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }

        public AnnouncementAssignRequestEvent(long requestId, long announcementId)
        {
            RequestId = requestId;
            AnnouncementId = announcementId;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }
        protected AnnouncementAssignRequestEvent()
        {
        }
    }
}