using MozaeekUserProfile.ApplicationService.Services.OtpCodeGeneratorServices;
using MozaeekUserProfile.ApplicationService.Services.SenderServices;
using MozaeekUserProfile.Domain.Contracts;
using System.Threading.Tasks;
using MozaeekUserProfile.Common.Constants;

namespace MozaeekUserProfile.ApplicationService.Services.OtpServices
{
    public class OtpStarterService : IOtpStarterService
    {
        private readonly ICodeSenderService _codeSenderService;
        private readonly IOtpCodeGenerator _otpCodeGenerator;
        private readonly IOtpCodeRepository _otpCodeStore;


        public OtpStarterService(ICodeSenderService codeSenderService,
                          IOtpCodeGenerator otpCodeGenerator,
                          IOtpCodeRepository otpCodeStore)
        {
            _codeSenderService = codeSenderService;
            _otpCodeGenerator = otpCodeGenerator;
            _otpCodeStore = otpCodeStore;
        }
        public async Task StartSession(string mobileNo)
        {
            var otpCode = _otpCodeGenerator.Generate(SettingConstants.OtpCodeLength);
            await _codeSenderService.Send(otpCode, mobileNo);
            await _otpCodeStore.Store(otpCode, mobileNo);
        }
    }
}
