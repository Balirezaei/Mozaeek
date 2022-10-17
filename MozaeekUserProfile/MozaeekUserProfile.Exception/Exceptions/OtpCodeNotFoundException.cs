using System;

namespace MozaeekUserProfile.Exception.Exceptions
{
    public class OtpCodeNotFoundException:ApplicationException
    {
        public OtpCodeNotFoundException()
        {

        }
        public OtpCodeNotFoundException(string message)
            :base(message)
        {

        }
    }
}
