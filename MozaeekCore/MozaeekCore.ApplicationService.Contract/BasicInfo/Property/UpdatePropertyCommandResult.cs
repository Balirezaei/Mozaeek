using MozaeekCore.Enum;

namespace MozaeekCore.ApplicationService.Contract
{
    public class UpdatePropertyCommandResult
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PropertyType PropertyType { get; set; }
        public PropertyDataType PropertyDataType { get; set; }
    }
}