using System;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract.BasicInfo
{
    public class PreRequestCreated : IEvent
    {
        public PreRequestCreated(long id, DateTime dateTime, string title, string summery, bool isProcessed)
        {
            Id = id;
            CreateDateTime = dateTime;
            Title = title;
            Summery = summery;
            IsProcessed = isProcessed;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }

        public long Id { get; private set; }
        public DateTime CreateDateTime { get; private set; }
        public string Title { get; private set; }
        public string Summery { get; private set; }
        public bool IsProcessed { get; private set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
        protected PreRequestCreated()
        {
        }
    }
}