using MozaeekCore.Core.Base;
using MozaeekCore.Enum;

namespace MozaeekCore.ApplicationService.Contract
{
    public class CreatePropertyCommand : Command
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public PropertyType PropertyType { get; set; }
        public PropertyDataType PropertyDataType { get; set; }
    }
}