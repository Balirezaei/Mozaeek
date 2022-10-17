using MozaeekTechnicianProfile.Core.Events;

namespace MozaeekTechnicianProfile.Domain.Contract.Request.Events
{
    public class UnProcessedRequestCreated : DomainEvent
    {
        public long Id { get; private set; }
        public string Title { get; private set; }
        public string Summery { get; private set; }
        public bool IsProcessed { get; private set; }

        public UnProcessedRequestCreated(long id, string title, string summery, bool isProcessed)
        {
            Id = id;
            Title = title;
            Summery = summery;
            IsProcessed = isProcessed;
        }
    }
}
