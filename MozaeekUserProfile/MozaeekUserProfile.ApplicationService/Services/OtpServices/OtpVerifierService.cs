using MozaeekUserProfile.Core.Core;
using MozaeekUserProfile.Domain;
using MozaeekUserProfile.Domain.Contracts;
using System.Threading.Tasks;
using MozaeekUserProfile.ApplicationService.Contract.Dtos;
using MozaeekUserProfile.Exception.Exceptions;

namespace MozaeekUserProfile.ApplicationService.Services.OtpServices
{
    public class OtpVerifierService : IOtpVerifierService
    {
        private readonly IOtpCodeRepository _otpCodeStore;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OtpVerifierService(IOtpCodeRepository otpCodeStore,
                                  IUserRepository userRepository,
                                  IUnitOfWork unitOfWork)
        {
            this._otpCodeStore = otpCodeStore;
            this._userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<UserLoginDto> Verify(string otpCode, string mobileNo, string refreshToken)
        {
            var codeIsValid = await _otpCodeStore.CheckAndDelete(mobileNo, otpCode);
            if (!codeIsValid)
            {
                throw new OtpCodeNotFoundException("Code Is Not Valid!");
            }

            var user = await _userRepository.AddIfNotExist(new User(mobileNo));
            user.UpdateRefreshToken(refreshToken);

            await _unitOfWork.CommitAsync();
            return new UserLoginDto()
            {
                FullName = user.FullName(),
                PhoneNumber = user.PhoneNumber,
                HasCompleted = user.ProfileIsCompleted,
                UserId = user.Id
            };
        }

        public async Task CheckRefreshTokenAndReplaceNew(long userId, string previouseRefreshToken, string newRefreshToken)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user.LastRefreshToken != previouseRefreshToken)
            {
                throw new System.Exception("رفرش توکن نادرست است.");
            }
            user.UpdateRefreshToken(newRefreshToken);
            await _unitOfWork.CommitAsync();
        }
    }
}
