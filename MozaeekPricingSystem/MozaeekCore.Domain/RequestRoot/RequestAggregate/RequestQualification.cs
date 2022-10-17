namespace MozaeekCore.Domain
{
    /// <summary>
    /// شرایط لازم
    /// </summary>
    public class RequestQualification
    {
        public string Description { get; private set; }
        public int Priority { get; private set; }
        public long Id { get; private set; }
        public RequestQualification(long id, string description, int priority)
        {
            Id = id;
            Description = description;
            Priority = priority;
        }
    }

    // public class RequestPoint
    // {
    //     public long Id { get; private set; }
    //     public long RequestId { get; set; }
    // }
}