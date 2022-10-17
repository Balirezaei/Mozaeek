using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class CreatePointCommand : Command
    {
        public string Title { get; set; }
        public long? ParentId { get; set; }
    }
}