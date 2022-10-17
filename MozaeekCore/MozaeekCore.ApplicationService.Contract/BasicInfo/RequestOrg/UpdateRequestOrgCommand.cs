using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class UpdateRequestOrgCommand : Command
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
    }
}