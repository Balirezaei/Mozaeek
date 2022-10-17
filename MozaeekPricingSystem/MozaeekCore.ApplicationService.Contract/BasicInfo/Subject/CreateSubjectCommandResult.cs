namespace MozaeekCore.ApplicationService.Contract
{
    public class CreateSubjectCommandResult
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long? ParentId { get; set; }
    }
}