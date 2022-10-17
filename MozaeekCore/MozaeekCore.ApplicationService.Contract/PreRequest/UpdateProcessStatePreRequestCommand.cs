using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class UpdateProcessStatePreRequestCommand : Command
    {
        public long Id { get; set; }
    }
}