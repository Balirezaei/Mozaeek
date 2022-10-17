using System;
using System.Collections.Generic;
using MozaeekCore.Core.Events;
using MozaeekCore.Domain.Contract.Request.Events;

namespace MozaeekCore.Domain.Contract
{
    public class AnnouncementCreatedOrUpdated : IEvent
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long RequestTargetId { get; set; }
        public List<long> PointIds { get; set; }
        public bool IsCreated { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
        protected AnnouncementCreatedOrUpdated() { }

        public AnnouncementCreatedOrUpdated(long id, string title, string description, long requestTargetId, List<long> pointIds, bool isCreated)
        {
            Id = id;
            Title = title;
            Description = description;
            RequestTargetId = requestTargetId;
            IsCreated = isCreated;
            PointIds = pointIds;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }
    }
}