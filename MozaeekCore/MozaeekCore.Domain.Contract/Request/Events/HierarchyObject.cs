namespace MozaeekCore.Domain.Contract.Request.Events
{
    public class HierarchyObject
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long? ParentId { get; set; }
    }
}