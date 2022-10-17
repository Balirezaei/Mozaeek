using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MozaeekUserProfile.ApplicationService.Contract.Dtos;
using MozaeekUserProfile.ApplicationService.Services;

namespace MozaeekUserProfile.RestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserProfileCharacteristicController : ControllerBase
    {
        private readonly IUserProfileCharacteristicService _userProfileCharacteristicService;

        public UserProfileCharacteristicController(IUserProfileCharacteristicService userProfileCharacteristicService)
        {
            _userProfileCharacteristicService = userProfileCharacteristicService;
        }

        [HttpPost]
        public Task<UserProfileCharacteristicOwnerCreateResult> CreateCharacteristicOwner([FromBody] UserProfileCharacteristicOwnerInput input)
        {
            return _userProfileCharacteristicService.CreateCharacteristicOwner(input);
        }

        [HttpPost]
        public Task<UserProfileCharacteristicOwnerCreateResult> UpdateCharacteristicOwner([FromBody] UserProfileCharacteristicOwnerUpdateInput input)
        {
            return _userProfileCharacteristicService.UpdateCharacteristicOwner(input);
        }

        [HttpPost]
        public Task<UserProfileCharacteristicCreateResult> CreateCharacteristic([FromBody] UserProfileCharacteristicInput input)
        {
            return _userProfileCharacteristicService.CreateCharacteristic(input);
        }

        [HttpPost]
        public Task<UserDashboardCharacteristicCreateResult> CreateUserDashboardCharacteristic([FromBody] UserDashboardCharacteristicInput input)
        {
            return _userProfileCharacteristicService.CreateUserDashboardCharacteristic(input);
        }

        [HttpGet("{id}")]
        public Task<UserProfileCharacteristicOwnerDeleteResult> DeleteCharacteristicOwner(int id)
        {
            return _userProfileCharacteristicService.DeleteUserProfileCharacteristicOwnerCreate(id);
        }

        [HttpGet]
        public Task<List<UserProfileCharacteristicOwnerDto>> GetAllCharacteristicOwner(long userId)
        {
            return _userProfileCharacteristicService.GetAllUserProfileCharacteristicOwner(userId);
        }
        [HttpGet]
        public Task<List<UserProfileCharacteristicSelectDto>> GetAllUserProfileCharacteristicDashboardSelect(long userId)
        {
            return _userProfileCharacteristicService.GetUserProfileCharacteristicDashboardSelect(userId);
        }

        [HttpGet]
        public Task<List<UserProfileCharacteristicDetail>> GetAllUserProfileCharacteristicByOwner(int ownerId)
        {
            return _userProfileCharacteristicService.GetUserProfileCharacteristicByOwner(ownerId);
        }

        [HttpPost]
        public Task<UserProfileCharacteristicDetail> GetCharacteristicPreSavedInLabelGroup(UserProfileCharacteristicPreSavedInLabelGroupInput input)
        {
            return _userProfileCharacteristicService.GetCharacteristicPreSavedInLabelGroup(input);
        }

    }
}
