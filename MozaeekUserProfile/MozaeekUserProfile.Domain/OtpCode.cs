using System;

namespace MozaeekUserProfile.Domain
{
    public class OtpCode
    {
        protected OtpCode()
        {
        }
        public OtpCode(string mobileNo, string code)
        {
            MobileNo = mobileNo;
            Code = code;
            ExpiredDate = DateTime.Now.AddSeconds(90);
        }
        public long Id { get; private set; }
        public string MobileNo { get; private set; }
        public string Code { get; private set; }
        public DateTime ExpiredDate { get; set; }


    }
}
