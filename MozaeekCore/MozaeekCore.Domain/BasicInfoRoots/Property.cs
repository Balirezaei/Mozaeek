using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Enum;

namespace MozaeekCore.Domain
{
    public class Property : BasicInfo
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public PropertyType PropertyType { get;private set; }
        public PropertyDataType PropertyDataType { get;private set; }

        public Property(string title, string description, PropertyType propertyType, PropertyDataType propertyDataType)
        {
            Title = title.Recheck();
            Description = description.Recheck();
            PropertyType = propertyType;
            PropertyDataType = propertyDataType;
        }

        public void Update(string title, string description, PropertyType propertyType, PropertyDataType propertyDataType)
        {
            Title = title.Recheck();
            Description = description.Recheck();
            PropertyType = propertyType;
            PropertyDataType = propertyDataType;
        }
    }

  
}