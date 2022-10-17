namespace MozaeekCore.ApplicationService.Contract
{
    public class CreatePointCommandResult
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long? ParentId { get; set; }
    }
}