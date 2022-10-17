using System;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract
{
    public class RequestPriceDeleted : IEvent
    {
        public long Id { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }

        public RequestPriceDeleted(long id)
        {
            Id = id;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }

        protected RequestPriceDeleted() { }
    }
}