namespace MozaeekCore.ApplicationService.Contract
{
    public class PropertyTotalCount
    {
        public long Count { get; private set; }
        public PropertyTotalCount(long count)
        {
            Count = count;
        }
    }
}