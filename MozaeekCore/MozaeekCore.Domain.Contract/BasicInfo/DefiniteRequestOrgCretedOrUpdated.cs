using System;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract.BasicInfo
{
    public class DefiniteRequestOrgCretedOrUpdated : IEvent
    {
        public long Id { get; set; }
        public long RequestOrgId { get; set; }
        public long PointId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
        public bool IsCreated { get; set; }

        public DefiniteRequestOrgCretedOrUpdated(long id, long requestOrgId, long pointId, string address, string phoneNumber, bool isCreated)
        {
            Id = id;
            RequestOrgId = requestOrgId;
            PointId = pointId;
            Address = address;
            PhoneNumber = phoneNumber;
            IsCreated = isCreated;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }

        protected DefiniteRequestOrgCretedOrUpdated()
        {

        }
    }
}