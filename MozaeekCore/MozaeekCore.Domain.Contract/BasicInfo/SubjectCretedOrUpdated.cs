using System;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract.BasicInfo
{
    public class SubjectCretedOrUpdated : IEvent
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public long? ParentId { get; set; }
        public bool IsCreated { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }

        public SubjectCretedOrUpdated(long id, string title,string icon, long? parentId, bool isCreated)
        {
            Id = id;
            Title = title;
            Icon = icon;
            ParentId = parentId;
            IsCreated = isCreated;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }

        protected SubjectCretedOrUpdated() { }
    }
}