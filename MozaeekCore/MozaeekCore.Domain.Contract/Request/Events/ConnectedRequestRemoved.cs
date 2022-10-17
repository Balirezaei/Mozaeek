using System;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract.Request.Events
{
    public class ConnectedRequestRemoved : IEvent
    {
        public long RequestId { get; set; }
        public long ConnectedRequestId { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }

        public ConnectedRequestRemoved(long requestId, long connectedRequestId)
        {
            RequestId = requestId;
            ConnectedRequestId = connectedRequestId;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }
    }
}