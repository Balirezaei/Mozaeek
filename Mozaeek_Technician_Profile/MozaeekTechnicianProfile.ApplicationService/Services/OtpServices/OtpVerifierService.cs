using System.Threading.Tasks;
using MozaeekTechnicianProfile.ApplicationService.Contract.Dtos;
using MozaeekTechnicianProfile.Core.Core;
using MozaeekTechnicianProfile.Domain;

namespace MozaeekTechnicianProfile.ApplicationService.Services.OtpServices
{
    public class OtpVerifierService : IOtpVerifierService
    {
        private readonly IOtpCodeRepository _otpCodeStore;
        private readonly ITechnicianRepository _technicianRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OtpVerifierService(IOtpCodeRepository otpCodeStore,
            ITechnicianRepository technicianRepository,
                                 IUnitOfWork unitOfWork
       )
        {
            this._otpCodeStore = otpCodeStore;
           this._technicianRepository = technicianRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<TechnicianLoginDto> Verify(string otpCode, string mobileNo, string refreshToken)
        {
            var codeIsValid = await _otpCodeStore.CheckOtpCode(mobileNo, otpCode);
            if (!codeIsValid)
            {
                throw new OtpCodeNotFoundException("Code Is Not Valid!");
            }

            var technician = await _technicianRepository.FindTechnicianByMobileNo(mobileNo);

            technician.UpdateRefreshToken(refreshToken);

            await _unitOfWork.CommitAsync();
            return new TechnicianLoginDto()
            {
                FullName = technician.FullName(),
                PhoneNumber = technician.PhoneNumber,
                TechnicianId = technician.Id
            };
        }

        public async Task CheckRefreshTokenAndReplaceNew(long technicianId, string previouseRefreshToken, string newRefreshToken)
        {
            var technician = await _technicianRepository.GetTechnicianById(technicianId);

            if (technician.LastRefreshToken != previouseRefreshToken)
            {
                throw new System.Exception("رفرش توکن نادرست است.");
            }
            technician.UpdateRefreshToken(newRefreshToken);
            await _unitOfWork.CommitAsync();
        }
    }
}
