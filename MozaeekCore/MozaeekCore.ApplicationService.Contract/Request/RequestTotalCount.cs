using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class RequestTotalCount  : Command
    {
        public long Count { get; private set; }

        public RequestTotalCount(long count)
        {
            Count = count;
        }
    }
}