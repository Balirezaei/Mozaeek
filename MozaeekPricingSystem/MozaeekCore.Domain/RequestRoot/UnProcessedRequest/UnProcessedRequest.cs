using System;
using MozaeekCore.Core.Domain;
using MozaeekCore.Domain.Contract.Request.Events;

namespace MozaeekCore.Domain
{
    public class UnProcessedRequest: AggregateRootBase
    {
        public DateTime CreateDateTime { get; private set; }
        public long Id { get; private set; }
        public string Title { get; private set; }
        public string Summery { get; private set; }
        public bool IsProcessed { get; private set; }
        protected UnProcessedRequest() { }
        public UnProcessedRequest(string title, string summery)
        {
            Title = title;
            Summery = summery;
            this.CreateDateTime = DateTime.Now;
            this.IsProcessed = false;
            //ToDo : Publish event in EventPublisher
            new UnProcessedRequestCreated(Id, Title, Summery, IsProcessed);
        }

    }
}