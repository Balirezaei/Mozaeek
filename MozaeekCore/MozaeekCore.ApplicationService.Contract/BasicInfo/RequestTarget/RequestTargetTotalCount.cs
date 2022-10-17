namespace MozaeekCore.ApplicationService.Contract
{
    public class RequestTargetTotalCount
    {
        public long Count { get; private set; }
        public RequestTargetTotalCount(long count)
        {
            Count = count;
        }
    }
}