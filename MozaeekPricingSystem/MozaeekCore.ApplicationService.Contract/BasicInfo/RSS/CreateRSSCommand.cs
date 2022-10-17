using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class CreateRSSCommand : Command
    {
        public string Url { get; set; }
        public string Source { get; set; }

        public int IntervalDataReceiveHours { get;  set; }
    }
}