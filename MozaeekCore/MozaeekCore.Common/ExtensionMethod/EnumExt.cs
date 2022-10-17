using System;
using System.ComponentModel.DataAnnotations;

namespace MozaeekCore.Common.ExtensionMethod
{
    public static class EnumExt
    {
        public static string GetDisplayValue(this Enum value)
        {
            try
            {
                var fieldInfo = value.GetType().GetField(value.ToString());
                var descriptionAttributes = fieldInfo.GetCustomAttributes(
                    typeof(DisplayAttribute), false) as DisplayAttribute[];
                if (descriptionAttributes == null) return string.Empty;
                return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
            }
            catch
            {
                return "";
            }

        }
    }
}