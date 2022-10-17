namespace MozaeekCore.ApplicationService.Contract
{
    public class PointGrid
    {
        public long Id { get; set; }
        public bool HasChild { get; set; }
        public string Title { get; set; }
    }

    public class AllLevelPointChildren
    {
        public long Id { get; set; }
    }
}