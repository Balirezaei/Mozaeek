namespace MozaeekCore.ApplicationService.Contract
{
    public class CreateRequestCommandResult
    {
        public long Id { get; set; }
    }

    public class RequestGrid
    {
        public long Id { get; set; }
        public string Title { get; set; }
    }
}