namespace MozaeekCore.ApplicationService.Contract
{
    public class PointDto
    {
        public string Title { get; set; }
        public long Id { get; set; }
        public long? ParentId { get; set; }
    }
}