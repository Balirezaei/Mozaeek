namespace MozaeekCore.Domain
{
    /// <summary>
    /// بایسته ها
    /// </summary>
    public class RequestNessesity
    {
        public RequestNessesity(long id, string description, int priority)
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