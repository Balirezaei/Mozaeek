namespace MozaeekCore.Domain.Contract.Request.Events
{
    public class RequestDependency
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }
}