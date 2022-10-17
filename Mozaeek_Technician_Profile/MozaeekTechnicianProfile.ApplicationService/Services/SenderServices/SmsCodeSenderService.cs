using System.Threading.Tasks;
using MozaeekTechnicianProfile.ApplicationService.Services.OtpServices;
using MozaeekTechnicianProfile.Domain;

namespace MozaeekTechnicianProfile.ApplicationService.Services.SenderServices
{
    public class SmsCodeSenderService : ICodeSenderService
    {
        private readonly IOtpSenderService otpSenderService;

        public SmsCodeSenderService(IOtpSenderService otpSenderService)
        {
            this.otpSenderService = otpSenderService;
        }
        public Task Send(string code, string destination)
        {
            return otpSenderService.SendOtp(destination, code);
        }
    }
}
