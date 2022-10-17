using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class PreRequestTotalCount : Command
    {
        public long Count { get; private set; }

        public PreRequestTotalCount(long count)
        {
            Count = count;
        }
    }
}