namespace MozaeekCore.ApplicationService.Contract
{
    public class RequestPriceTotalCount
    {
        public long Count { get; private set; }

        public RequestPriceTotalCount(long count)
        {
            Count = count;
        }
    }
}