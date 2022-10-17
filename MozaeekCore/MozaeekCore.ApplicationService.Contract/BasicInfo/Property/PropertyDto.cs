using MozaeekCore.Enum;

namespace MozaeekCore.ApplicationService.Contract
{
    public class PropertyDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PropertyType PropertyType { get; set; }
        public string PropertyTypeDescription { get; set; }
        public PropertyDataType PropertyDataType { get; set; }
        public string PropertyDataTypeDescription { get; set; }
    }
}