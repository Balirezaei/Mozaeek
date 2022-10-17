using System.Threading.Tasks;
using MozaeekTechnicianProfile.ApplicationService.Contract.Dtos;

namespace MozaeekTechnicianProfile.ApplicationService.Services.OtpServices
{
    public interface IOtpVerifierService
    {
        Task<TechnicianLoginDto> Verify(string otpCode, string mobileNo,string refreshToken);
         Task CheckRefreshTokenAndReplaceNew(long technicianId, string previouseRefreshToken, string newRefreshToken);
    }
}
