using System;
using System.Globalization;

namespace Karmizban.Support.Common
{
    public static class DateTimeExt
    {
        public static DateTime S2M(String convDate)
        {
            DateTime returnValue;
            var jc = new PersianCalendar();

            if (convDate.Length == 8 & convDate.Contains("/") && !convDate.StartsWith("13"))
                convDate = "13" + convDate;
            if (convDate.Length < 8)
            {
                returnValue = DateTime.Parse("1900/01/01 12:00:00 AM");
                return returnValue;
            }

            try
            {
                returnValue = jc.ToDateTime(int.Parse(convDate.Substring(0, 4)),
                    int.Parse(convDate.Substring(5, 2)),
                    int.Parse(convDate.Substring(8, 2)), 1, 1, 1, 1);
            }
            catch
            {
                var dt1 = jc.ToDateTime(int.Parse(convDate.Split('/')[0]), int.Parse(convDate.Split('/')[1])
                    , int.Parse(convDate.Split('/')[2]), 0, 0, 0, 0);

                returnValue = dt1;
            }
            return returnValue;
        }
        //1395/09/27  00:10:35
        public static DateTime S2MWithTime(String convDate)
        {
            DateTime returnValue;
            var jc = new PersianCalendar();

            if (convDate.Length == 8 & convDate.Contains("/") && !convDate.StartsWith("13"))
                convDate = "13" + convDate;
            if (convDate.Length < 8)
            {
                returnValue = DateTime.Parse("1900/01/01 12:00:00 AM");
                return returnValue;
            }

            try
            {
                returnValue = jc.ToDateTime(int.Parse(convDate.Substring(0, 4)),
                    int.Parse(convDate.Substring(5, 2)),
                    int.Parse(convDate.Substring(8, 2)),
                    int.Parse(convDate.Substring(12, 2)),
                    int.Parse(convDate.Substring(15, 2)),
                    int.Parse(convDate.Substring(18, 2)), 1);
            }
            catch
            {
                var dt1 = jc.ToDateTime(int.Parse(convDate.Split('/')[0]), int.Parse(convDate.Split('/')[1])
                    , int.Parse(convDate.Split('/')[2]), 0, 0, 0, 0);

                returnValue = dt1;
            }
            return returnValue;
        }

        public static String M2SShowTime(DateTime convDate)
        {
            var jc = new PersianCalendar();
            if (convDate.ToString(CultureInfo.InvariantCulture) == "12:00:00 AM")
            {
                return "1300/01/01";
            }
            try
            {
                var farsiMinute = Convert.ToString(jc.GetMinute(convDate));
                var farsiHour = Convert.ToString(jc.GetHour(convDate));
                //var farsiYear = Convert.ToString(jc.GetYear(convDate));
                //var farsiMonth = Convert.ToString(jc.GetMonth(convDate));
                //var farsiDay = Convert.ToString(jc.GetDayOfMonth(convDate));
                //farsiDay = farsiDay.PadLeft(2, '0');
                //farsiMonth = farsiMonth.PadLeft(2, '0');
                var returnValue = farsiHour + ":" + farsiMinute;
                return returnValue.ToPersianNumber();
            }
            catch
            {
                return "1300/01/01";
            }
        }
        public static DateTime GenerateDataTime(string yearNumber, string monthNumber, string dateNumber)
        {
            var cal = new PersianCalendar();
            var dt1 = cal.ToDateTime(int.Parse(yearNumber), int.Parse(monthNumber), int.Parse(dateNumber), 0, 0, 0,
                0);

            return dt1;
        }

        public static String M2S(DateTime convDate)
        {
            var jc = new PersianCalendar();
            if (convDate.ToString(CultureInfo.InvariantCulture) == "12:00:00 AM")
            {
                return "1300/01/01";
            }
            try
            {
                var farsiYear = Convert.ToString(jc.GetYear(convDate));
                var farsiMonth = Convert.ToString(jc.GetMonth(convDate));
                var farsiDay = Convert.ToString(jc.GetDayOfMonth(convDate));
                farsiDay = farsiDay.PadLeft(2, '0');
                farsiMonth = farsiMonth.PadLeft(2, '0');
                var returnValue = farsiYear + "/" + farsiMonth + "/" + farsiDay;
                return returnValue;
            }
            catch
            {
                return "1300/01/01";
            }
        }

        public static String M2SWithTime(DateTime convDate)
        {
            var r = string.Format("{0} {1:D2}:{2:D2}:{3:D2}", M2S(convDate), convDate.Hour, convDate.Minute, convDate.Second);
            return r;
        }
        public static string JumpToLastDay(string sDate)
        {
            if (sDate == null) return "Error";
            if (sDate.Trim() == "") return "Error";
            var jc = new PersianCalendar();
            var str = sDate.Split('/');
            var temMonth = int.Parse(str[1]);
            var temDay = int.Parse(str[2]);
            var temYear = int.Parse(str[0]);
            var isLeapYear = jc.IsLeapYear(temYear);
            if (temMonth >= 1 & temMonth <= 6)
                temDay = 31;
            if (temMonth >= 7 & temMonth <= 11)
                temDay = 30;
            if (temMonth == 12 & isLeapYear)
            {
                temDay = 30;
            }
            else if (temMonth == 12 & isLeapYear == false)
            {
                temDay = 29;
            }
            return temYear + "/" + temMonth.ToString("00") + "/" + temDay.ToString("00");
        }


        public static string GetStringOfDay(int day)
        {
            switch (day)
            {
                case 1:
                    return "یکم";
                case 2:
                    return "دوم";
                case 3:
                    return "سوم";
                case 4:
                    return "چهارم";
                case 5:
                    return "پنجم";
                case 6:
                    return "ششم";
                case 7:
                    return "هفتم";
                case 8:
                    return "هشتم";
                case 9:
                    return "نهم";
                case 10:
                    return "دهم";
                case 11:
                    return "یازدهم";
                case 12:
                    return "دوازدهم";
                case 13:
                    return "سیزدهم";
                case 14:
                    return "چهاردهم";
                case 15:
                    return "پانزدهم";
                case 16:
                    return "شانزدهم";
                case 17:
                    return "هفدهم";
                case 18:
                    return "هجدهم";
                case 19:
                    return "نوزدهم";
                case 20:
                    return "بیستم";
                case 21:
                    return "بیست و یکم";
                case 22:
                    return "بیست و دوم";
                case 23:
                    return "بیست و سوم";
                case 24:
                    return "بیست و چهارم";
                case 25:
                    return "بیست و پنجم";
                case 26:
                    return "بیست و ششم";
                case 27:
                    return "بیست و هفتم";
                case 28:
                    return "بیست و هشتم";
                case 29:
                    return "بیست و نهم";
                case 30:
                    return "سی ام ";
                case 31:
                    return "سی و یکم";
                default:
                    return "";
            }
        }

        public static string GetStringOfMonth(int month)
        {
            switch (month)
            {
                case 1:
                    return "فروردین";
                case 2:
                    return "اردیبهشت";
                case 3:
                    return "خرداد";
                case 4:
                    return "تیر";
                case 5:
                    return "مرداد";
                case 6:
                    return "شهریور";
                case 7:
                    return "مهر";
                case 8:
                    return "آبان";
                case 9:
                    return "آذر";
                case 10:
                    return "دی";
                case 11:
                    return "بهمن";
                case 12:
                    return "اسفند";
                default:
                    return "";
            }
        }


        public static DateTime GetCurrentYearFirstDay()
        {
            var year = DateTime.Now.Year;
            if ((DateTime.Now.Month == 1 || DateTime.Now.Month == 2) || (DateTime.Now.Month == 3 && DateTime.Now.Month < 21))
            {
                year = year - 1;
            }
            var d = new DateTime(year, 3, 21);
            return d;
        }


        public static DateTime GetFirstDayOfYear(int year)
        {
            var jc = new PersianCalendar();
            return jc.ToDateTime(year, 1, 1, 0, 0, 0, 0);
        }

        public static DateTime GetEndDayOfYear(int year)
        {
            var jc = new PersianCalendar();
            var dt = jc.ToDateTime(year, 12, jc.IsLeapYear(year) ? 30 : 29, 1, 1, 1, 1);
            return dt;
        }

        public static string GetTimeFromNow(this DateTime date)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = DateTime.Now - date;
            double delta = Math.Abs(ts.TotalSeconds);
            if ((DateTime.Now - date).Days > 6)
            {
                return M2S(date).ToPersianNumber();
            }

            var result = "";
            if (delta < 1 * MINUTE)
                result = ts.Seconds == 1 ? "1 ثانیه قبل" : ts.Seconds + " ثانیه قبل";

            else if (delta < 2 * MINUTE)
                result = "1 دقیقه قبل";

            else if (delta < 45 * MINUTE)
                result = ts.Minutes + " دقیقه قبل";

            else if (delta < 90 * MINUTE)
                result = "1 ساعت قبل";

            else if (delta < 24 * HOUR)
                result = ts.Hours + " ساعت قبل";

            else if (delta < 48 * HOUR)
                result = "دیروز";

            else if (delta < 30 * DAY)
                result = ts.Days + " روز قبل";

            else if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                result = months <= 1 ? "1 ماه قبل" : months + " ماه قبل";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                result = years <= 1 ? "1 سال قبل" : years + " سال قبل";
            }

            return result.ToPersianNumber();

        }
    }
}