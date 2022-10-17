using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class CreateRequestActCommand : Command
    {
        public string Title { get; set; }
    }
}