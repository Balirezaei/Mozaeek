using System;

namespace MozaeekTechnicianProfile
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
