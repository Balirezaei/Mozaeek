namespace MozaeekCore.ApplicationService.Contract
{
    public class UpdateSubjectCommandResult
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long? ParentId { get; set; }
    }
}