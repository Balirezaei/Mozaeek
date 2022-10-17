using System;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract.BasicInfo
{
    public class PropertyDeleted : IEvent
    {
        public PropertyDeleted(long id)
        {
            Id = id;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }
        protected PropertyDeleted()
        {

        }
        public long Id { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
    }
}