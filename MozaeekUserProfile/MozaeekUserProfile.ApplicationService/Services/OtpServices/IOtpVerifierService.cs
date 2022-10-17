using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MozaeekUserProfile.ApplicationService.Contract.Dtos;

namespace MozaeekUserProfile.ApplicationService.Services.OtpServices
{
    public interface IOtpVerifierService
    {
        Task<UserLoginDto> Verify(string otpCode, string mobileNo,string refreshToken);
         Task CheckRefreshTokenAndReplaceNew(long userId, string previouseRefreshToken, string newRefreshToken);
    }
}
