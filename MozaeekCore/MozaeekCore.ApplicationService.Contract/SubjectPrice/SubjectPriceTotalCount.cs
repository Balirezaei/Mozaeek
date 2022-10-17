namespace MozaeekCore.ApplicationService.Contract
{
    public class SubjectPriceTotalCount
    {
        public long Count { get; private set; }

        public SubjectPriceTotalCount(long count)
        {
            Count = count;
        }
    }
}