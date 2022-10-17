namespace MozaeekCore.Domain
{
    public class RequestTargetConnection
    {
        protected RequestTargetConnection()
        {

        }

        public RequestTargetConnection(long id, long requestTargetId, string description)
        {
            Id = id;
            RequestTargetId = requestTargetId;
            Description = description;
        }

        public long Id { get; private set; }
        public long RequestTargetId { get; private set; }
        public string Description { get; private set; }

    }
}