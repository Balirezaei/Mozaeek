using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekUserProfile.Domain.Contracts
{
    public interface IOtpCodeRepository
    {
        Task Store(string otpCode, string mobileNo);
        Task<bool> CheckAndDelete(string mobileNo,string code);
    }
}
