using System;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract
{
    public class SubjectPriceDeleted : IEvent
    {
        public long Id { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }

        public SubjectPriceDeleted(long id)
        {
            Id = id;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }

        protected SubjectPriceDeleted() { }
    }
}