using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class UpdatePreRequestCommand : Command
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
    }
}