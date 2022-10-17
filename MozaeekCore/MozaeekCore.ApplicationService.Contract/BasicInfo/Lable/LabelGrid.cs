namespace MozaeekCore.ApplicationService.Contract
{
    public class LabelGrid
    {
        public long Id { get; set; }
        public bool HasChild { get; set; }
        public string Title { get; set; }

    }

    public class AllLevelLabelChildren
    {
        public long Id { get; set; }
    }
}