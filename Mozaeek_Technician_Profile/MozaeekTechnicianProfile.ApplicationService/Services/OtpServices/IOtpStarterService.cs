using System.Threading.Tasks;

namespace MozaeekTechnicianProfile.ApplicationService.Services.OtpServices
{
    public interface IOtpStarterService
    {
        Task StartSession(string mobileNo);
    }
}
