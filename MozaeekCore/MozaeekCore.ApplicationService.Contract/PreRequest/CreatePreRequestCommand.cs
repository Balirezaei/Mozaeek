using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class CreatePreRequestCommand : Command
    {
        public string Title { get; set; }
        public string Summary { get; set; }
    }
}