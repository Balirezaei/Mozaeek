namespace MozaeekCore.ApplicationService.Contract
{
    public class SubjectTotalCount
    {
        public long Count { get; private set; }

        public SubjectTotalCount(long count)
        {
            Count = count;
        }
    }
}