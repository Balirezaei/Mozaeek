using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekUserProfile.Domain.Contracts
{
    public interface IOtpSenderService
    {
        Task SendOtp(string phoneNumber, string message);
    }
}
