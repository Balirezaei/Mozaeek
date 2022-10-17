namespace MozaeekCore.Domain
{
    /// <summary>
    /// مدارک لازم
    /// </summary>
    public class RequestDocument
    {
        public RequestDocument(long id, string description, int priority)
        {
            Id = id;
            Description = description;
            Priority = priority;
        }
        public long Id { get; private set; }
        public string Description { get;private set; }
        public int Priority { get;private set; }
    }
}