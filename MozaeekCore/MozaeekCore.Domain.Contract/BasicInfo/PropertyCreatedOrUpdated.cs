using System;
using MozaeekCore.Core.Events;
using MozaeekCore.Enum;

namespace MozaeekCore.Domain.Contract.BasicInfo
{
    public class PropertyCreatedOrUpdated : IEvent
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PropertyType PropertyType { get; set; }
        public PropertyDataType PropertyDataType { get; set; }
        public bool IsCreated { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }

        public PropertyCreatedOrUpdated(long id, string title, string description, PropertyType propertyType, PropertyDataType propertyDataType, bool isCreated)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.PropertyType = propertyType;
            this.PropertyDataType = propertyDataType;
            IsCreated = isCreated;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }

        protected PropertyCreatedOrUpdated()
        {

        }
    }
}