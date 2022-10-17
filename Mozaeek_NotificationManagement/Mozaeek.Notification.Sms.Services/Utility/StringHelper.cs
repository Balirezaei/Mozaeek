using System;
using System.Collections.Generic;
using System.Text;

namespace Mozaeek.Notification.Sms.Services.Utility
{
    public class StringHelper
    {
        public static string NormalizeMobileNumber(string mobileNumber)
        {
            if (mobileNumber.StartsWith("0"))
            {
                mobileNumber = mobileNumber.Remove(0, 1);
                return "98" + mobileNumber;
            }
            if (mobileNumber.StartsWith("+"))
            {
                mobileNumber = mobileNumber.Remove(0, 1);
                return mobileNumber;
            }
            return mobileNumber;
        }
    }
}
