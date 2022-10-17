using System;

namespace Mozaeek.CR.PublicEvent.UserProfile
{
    public class BaseEvent
    {
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
    }
}