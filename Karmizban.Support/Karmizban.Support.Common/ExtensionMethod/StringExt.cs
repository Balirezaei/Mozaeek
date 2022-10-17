namespace Karmizban.Support.Common
{
    public static class StringExt
    {
        public static bool IsNullOrEmpty(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            else
            {
                return false;
            }
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

        /// <summary>
        /// متدی برای تبدیل اعداد انگلیسی به فارسی
        /// </summary>
        public static string ToPersianNumber(this string input)
        {
            if (input.Trim() == "") return "";

            //۰ ۱ ۲ ۳ ۴ ۵ ۶ ۷ ۸ ۹
            input = input.Replace("0", "۰");
            input = input.Replace("1", "۱");
            input = input.Replace("2", "۲");
            input = input.Replace("3", "۳");
            input = input.Replace("4", "۴");
            input = input.Replace("5", "۵");
            input = input.Replace("6", "۶");
            input = input.Replace("7", "۷");
            input = input.Replace("8", "۸");
            input = input.Replace("9", "۹");
            return input;
            //return String.Format(Format, input);
        }
    }
}