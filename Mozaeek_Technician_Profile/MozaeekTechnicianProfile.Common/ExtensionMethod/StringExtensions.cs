using System;
using MozaeekTechnicianProfile.Common.Constants;

namespace MozaeekTechnicianProfile.Common.ExtensionMethod
{
    public static class StringExtensions
    {

        public static void CheckIsMobileNo(this string input)
        {
            if (input.Length != SettingConstants.MobileNoLength)
            {
                throw new InvalidInputException($"Mobile No Length Must Be {SettingConstants.MobileNoLength}");
            }
            if (!input.IsDigit())
            {
                throw new InvalidInputException($"Mobile No Must be digit!");
            }
        }
        public static bool IsDigit(this string value)
        {
            foreach (var ch in value.ToCharArray())
            {
                if (!Char.IsDigit(ch))
                {
                    return false;
                }
            }
            return true;
        }

        public static string Recheck(this string str)
        {
            if (str == null)
            {
                return null;
            }
            if (str.Contains("ك"))
            {
                str = str.Replace("ك", "ک");
            }
            if (str.Contains("ي"))
            {
                str = str.Replace("ي", "ی");
            }
            if (str.Contains("ة"))
            {
                str = str.Replace("ة", "ه");
            }

            str = str.Trim().TrimEnd();

            return str;
        }
    }
}