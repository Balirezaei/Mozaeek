namespace MozaeekCore.ApplicationService.Contract
{
    public class RSSTotalCount
    {
        public long Count { get; private set; }
        public RSSTotalCount(long count)
        {
            Count = count;
        }
    }
}