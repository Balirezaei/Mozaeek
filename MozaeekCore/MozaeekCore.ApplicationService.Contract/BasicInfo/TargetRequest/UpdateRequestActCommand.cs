using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class UpdateRequestActCommand : Command
    {
        public long Id { get; set; }
        public string Title { get; set; }
    }
}