namespace MozaeekCore.ApplicationService.Contract
{
    public class SubjectGrid
    {
        public long Id { get; set; }
        public bool HasChild { get; set; }
        public string Title { get; set; }

    }

    public class AllLevelSubjectChildren
    {
        public long Id { get; set; }
    }
}