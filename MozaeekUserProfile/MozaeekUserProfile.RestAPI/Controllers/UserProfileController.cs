using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MozaeekUserProfile.ApplicationService.Services.UserProfileServices;
using MozaeekUserProfile.Core.Core.ResponseMessages;
using System.Threading.Tasks;
using MozaeekUserProfile.ApplicationService.Contract.Dtos;

namespace MozaeekUserProfile.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }
        [HttpPost("update")]
        //   [Authorize]
        public async Task<bool> CompleteProfile(UserProfileCompleteInputeDto inputeDto)
        {
            await userProfileService.CompleteProfile(inputeDto);
            return true;
        }

        [HttpPost("read/{id?}")]
        // [Authorize]
        public async Task<UserProfileOutputDto> Get(long id)
        {
            return await userProfileService.GetCurrentUserProfile(id);
        }
        [HttpPost("UpdateDeviceId")]
        public async Task<bool> UpdateDeviceId(UserProfileUpdateDeviceIdDto inputDto)
        {
            await userProfileService.UpdateDeviceId(inputDto);
            return true;
        }
    }
}
