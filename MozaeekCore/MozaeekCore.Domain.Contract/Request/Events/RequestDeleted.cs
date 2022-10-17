using System;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract.Request.Events
{
    public class RequestDeleted : IEvent
    {
        public long Id { get; set; }

        public RequestDeleted(long id)
        {
            Id = id; 
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
    }
}