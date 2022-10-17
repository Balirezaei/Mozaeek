namespace MozaeekCore.ApplicationService.Contract
{
    public class PointTotalCount
    {
        public long Count { get; private set; }
        public PointTotalCount(long count)
        {
            Count = count;
        }
    }
}