using System.Threading.Tasks;

namespace MozaeekTechnicianProfile.Domain
{
    public interface IOtpCodeRepository
    {
        Task Store(string otpCode, string mobileNo);
        Task<bool> CheckOtpCode(string mobileNo, string code);
    }
}