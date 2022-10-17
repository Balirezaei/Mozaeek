using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class DeleteDefiniteRequestOrgCommand : Command
    {
        public long Id { get; set; }
    }
}