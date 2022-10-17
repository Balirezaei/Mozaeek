using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MozaeekUserProfile.ApplicationService.Contract.Dtos;
using MozaeekUserProfile.ApplicationService.Services.UserDashboardService;
using MozaeekUserProfile.Core.Core.ResponseMessages;

namespace MozaeekUserProfile.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //  [Authorize]
    public class UserDashboardController : ControllerBase
    {
        private readonly IUserDashboardService _userDashboardService;

        public UserDashboardController(IUserDashboardService userDashboardService)
        {
            _userDashboardService = userDashboardService;
        }

        [HttpGet("read")]
        public async Task<List<UserDashboardDto>> Get(long userId)
        {
            var res = await _userDashboardService.GetAllWorkbenchUserDashboardWithCharactreristic(userId);
            return res;
        }

        [HttpPost("create")]
        public async Task<UserDashboardCreateResult> Create(UserDashboardInputDto input)
        {
            return await _userDashboardService.CreateUserDashboard(input);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> RemoveUserDashboard(long userDashboardId)
        {
            await _userDashboardService.RemoveUserDashboard(userDashboardId);
            return Ok();
        }

        [HttpPost("RemoveUserDashboardCharacteristic")]
        public async Task<IActionResult> RemoveUserDashboardCharacteristic(int ownerId)
        {
            await _userDashboardService.RemoveUserDashboardCharacteristic(ownerId);
            return Ok();
        }
        [HttpGet("GetUserDashboardCharacteristic")]
        public Task<List<CharacteristicUserDashboardDto>> GetUserDashboardCharacteristic(int ownerId)
        {
            return _userDashboardService.GetUserDashboardCharacteristic(ownerId);
        }
    }
}