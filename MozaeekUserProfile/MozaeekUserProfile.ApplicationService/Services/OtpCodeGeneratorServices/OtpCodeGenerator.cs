using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekUserProfile.ApplicationService.Services.OtpCodeGeneratorServices
{
    public class OtpCodeGenerator : IOtpCodeGenerator
    {
        public string Generate(int length)
        {
            StringBuilder randomString = new StringBuilder();
            int count = 0;
            var rnd = new Random();
            do
            {
               randomString.Append(rnd.Next(10));
                count++;
            } while (count < length);
            return randomString.ToString();
        }
    }
}
