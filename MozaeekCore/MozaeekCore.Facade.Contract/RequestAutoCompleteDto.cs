namespace MozaeekCore.Facade.Contract
{
    public class RequestAutoCompleteDto
    {
        public string Title { get; set; }
        public long[] ExcludeRequestIds { get; set; }
    }

    public class RequestTargetAutoCompleteDto
    {
        public string Title { get; set; }
        public long[] ExcludeRequestTargetIds { get; set; }
    }
}