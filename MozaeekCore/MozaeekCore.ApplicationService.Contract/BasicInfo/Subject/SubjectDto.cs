namespace MozaeekCore.ApplicationService.Contract
{
    /// <summary>
    /// موضوعات
    /// </summary>
    public class SubjectDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long? ParentId { get; set; }
        public string Icon { get; set; }
    }
}