namespace MozaeekCore.Domain
{
    /// <summary>
    /// اقدامات لازم
    /// </summary>
    public class RequestAction
    {
        public long Id { get; private set; }
        public string Description { get;private set; }
        public int Priority { get;private set; }

        public RequestAction(long id, string description, int priority)
        {
            Id = id;
            Description = description;
            Priority = priority;
        }
    }
}