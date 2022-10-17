using System.Threading.Tasks;
using MozaeekUserProfile.ApplicationService.Contract.Dtos;

namespace MozaeekUserProfile.ApplicationService.Services.UserProfileServices
{
    public interface IUserProfileService
    {
        Task CompleteProfile(UserProfileCompleteInputeDto inputeDto);
        Task<UserProfileOutputDto> GetCurrentUserProfile(long userId);
        Task UpdateDeviceId(UserProfileUpdateDeviceIdDto inputeDto);
    }
}
