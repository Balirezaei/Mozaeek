using System.Threading.Tasks;
using MozaeekTechnicianProfile.ApplicationService.Services.SenderServices;
using MozaeekTechnicianProfile.Common.Constants;
using MozaeekTechnicianProfile.Domain;

namespace MozaeekTechnicianProfile.ApplicationService.Services.OtpServices
{
    public class OtpStarterService : IOtpStarterService
    {
        private readonly ICodeSenderService _codeSenderService;
        private readonly IOtpCodeGenerator _otpCodeGenerator;
        private readonly IOtpCodeRepository _otpCodeStore;
        private readonly ITechnicianRepository _technicianRepository;

        public OtpStarterService(ICodeSenderService codeSenderService,
                          IOtpCodeGenerator otpCodeGenerator,
                          IOtpCodeRepository otpCodeStore, ITechnicianRepository technicianRepository)
        {
            _codeSenderService = codeSenderService;
            _otpCodeGenerator = otpCodeGenerator;
            _otpCodeStore = otpCodeStore;
            _technicianRepository = technicianRepository;
        }
        public async Task StartSession(string mobileNo)
        {
            var technician = await _technicianRepository.FindTechnicianByMobileNo(mobileNo);
            if (technician==null)
            {
                throw new System.Exception("شماره موبایل شما ثبت نام نشده است.");
            }
            var otpCode = _otpCodeGenerator.Generate(SettingConstants.OtpCodeLength);
            await _codeSenderService.Send(otpCode, mobileNo);
            await _otpCodeStore.Store(otpCode, mobileNo);
        }
    }
}
