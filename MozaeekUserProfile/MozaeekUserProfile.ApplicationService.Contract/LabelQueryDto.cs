namespace MozaeekUserProfile.ApplicationService.Contract
{
    public class LabelQueryDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool HasChild { get; set; }
    }
    public class SubjectQueryDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool HasChild { get; set; }
    }
    public class RequestTargetQueryDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool HasChild { get; set; }
    }
    public class RequestOrgQueryDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool HasChild { get; set; }
    }

    public class PointQueryDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool HasChild { get; set; }
    }
}