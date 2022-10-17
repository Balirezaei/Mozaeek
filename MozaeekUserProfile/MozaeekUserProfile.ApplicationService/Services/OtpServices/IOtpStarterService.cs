using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekUserProfile.ApplicationService.Services.OtpServices
{
    public interface IOtpStarterService
    {
        Task StartSession(string mobileNo);
    }
}
