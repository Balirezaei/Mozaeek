using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mozaeek.CR.PublicDto.Dto;
using MozaeekUserProfile.ApplicationService.Services.UserAnnouncementServices;

namespace MozaeekUserProfile.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnoucementUserDashboardController : ControllerBase
    {
        private readonly IUserAnnouncementService _userAnnouncementService;

        public AnnoucementUserDashboardController(IUserAnnouncementService userAnnouncementService)
        {
            _userAnnouncementService = userAnnouncementService;
        }
        [HttpGet("Get")]
        public async Task<AnnouncementUserDashboardDto> Get(long userId)
        {
            return await _userAnnouncementService.GetAnnouncementUserDashboard(userId);
        }
    }
}
