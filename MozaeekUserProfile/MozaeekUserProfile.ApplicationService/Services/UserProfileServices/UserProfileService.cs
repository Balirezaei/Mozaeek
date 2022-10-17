using MozaeekUserProfile.Domain.Contracts;
using System.Threading.Tasks;
using MozaeekUserProfile.ApplicationService.Contract.Dtos;
using MozaeekUserProfile.Core.Core;
using MozaeekUserProfile.Mapper;

namespace MozaeekUserProfile.ApplicationService.Services.UserProfileServices
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserRepository _userRepository;
        private readonly CurrentUser _currentUser;
        private readonly IUnitOfWork _unitOfWork;

        public UserProfileService(IUserRepository userRepository,
                                  CurrentUser currentUser, IUnitOfWork unitOfWork)
        {
            this._userRepository = userRepository;
            this._currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }
        public async Task CompleteProfile(UserProfileCompleteInputeDto inputeDto)
        {
            var user = await _userRepository.GetUserById(inputeDto.UserId);
            user.UpdateInformation(inputeDto.FirstName, inputeDto.LastName, inputeDto.Email, inputeDto.Address);
            _userRepository.UpdateProfile(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task<UserProfileOutputDto> GetCurrentUserProfile(long userId)
        {
            var user = await _userRepository.GetUserById(userId);
            return user.GetUserInput();
        }
        public async Task UpdateDeviceId(UserProfileUpdateDeviceIdDto inputeDto)
        {
            var user = await _userRepository.GetUserById(inputeDto.UserId);
            user.UpdateDeviceId(inputeDto.DeviceId);
            _userRepository.UpdateProfile(user);
            await _unitOfWork.CommitAsync();
        }

    }
}
