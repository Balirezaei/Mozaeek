namespace MozaeekCore.ApplicationService.Contract
{
    public class RequestActTotalCount
    {
        public long Count { get; private set; }
        public RequestActTotalCount(long count)
        {
            Count = count;
        }
    }
}