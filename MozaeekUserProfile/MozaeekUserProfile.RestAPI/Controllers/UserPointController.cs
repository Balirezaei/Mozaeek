using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekUserProfile.ApplicationService.Contract;
using MozaeekUserProfile.ApplicationService.Services.UserPoint;
using MozaeekUserProfile.Core.Core.ResponseMessages;

namespace MozaeekUserProfile.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPointController : ControllerBase
    {
        private readonly IUserPointService _userPointService;

        public UserPointController(IUserPointService userPointService)
        {
            _userPointService = userPointService;
        }
        [HttpGet("GetActiveUserPoint")]
        public async Task<UserPointInputDto> GetActiveUserPoint(long userId)
        {
            return await _userPointService.GetCurrentUserPoint(userId);
        }

        [HttpPost("Create")]
        public async Task<UserPointInputDto> Create(UserPointInputDto inputDto)
        {
            return await _userPointService.CreateUserPoint(inputDto);
        }
    }
}
