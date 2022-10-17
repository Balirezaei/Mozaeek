using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekUserProfile.ApplicationService.Services.OtpCodeGeneratorServices
{
    public interface IOtpCodeGenerator
    {
        string Generate(int length);
    }
}
