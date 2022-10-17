using System.Threading.Tasks;

namespace MozaeekTechnicianProfile.ApplicationService.Services
{
    public interface IOtpSenderService
    {
        Task SendOtp(string phoneNumber, string message);
    }
}