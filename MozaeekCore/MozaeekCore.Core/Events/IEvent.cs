using System;

namespace MozaeekCore.Core.Events
{
    public interface IEvent
    {
        Guid EventId { get; set; }
        DateTime PublishDateTime { get; set; }
    }
}
