using System;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract.BasicInfo
{
    public class RequestTargetDeleted : IEvent
    {
        public long Id { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }

        public RequestTargetDeleted(long id)
        {
            Id = id;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }

        protected RequestTargetDeleted() { }
    }
}