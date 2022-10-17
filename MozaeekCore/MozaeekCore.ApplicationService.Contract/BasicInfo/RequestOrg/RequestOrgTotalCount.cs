namespace MozaeekCore.ApplicationService.Contract
{
    public class RequestOrgTotalCount
    {
        public long Count { get; private set; }

        public RequestOrgTotalCount(long count)
        {
            Count = count;
        }
    }
}