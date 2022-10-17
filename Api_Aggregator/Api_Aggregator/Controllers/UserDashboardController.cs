using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Api_Aggregator.Contract.MediationDtos.MozaeekCore.BasicInfoDtos;
using Api_Aggregator.ApplicationService.AggregationServices.UserAnnoucementBasicInfo;
using Api_Aggregator.Contract.MediationDtos;

namespace Api_Aggregator.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DashboardController : ControllerBase
    {

        private readonly IAnnoucementUserDashboardBasicInfoAggregationService _userAnnouncementBasicInfoAggregationService;

        public DashboardController(IAnnoucementUserDashboardBasicInfoAggregationService userAnnouncementBasicInfoAggregationService)
        {
            _userAnnouncementBasicInfoAggregationService = userAnnouncementBasicInfoAggregationService;
        }

        [HttpGet]
        public async Task<List<UserAnnouncementDto>> GetUserAnnouncement(int id)
        {
            return await _userAnnouncementBasicInfoAggregationService.GetUserAnnoucement(id);
        }
        [HttpPost]
        public Task<UserSearchResult> FullSearchByUserCharacteristics([FromBody] FullUserSearchByCharacteristic input)
        {
            return _userAnnouncementBasicInfoAggregationService.FullSearchByUserCharacteristics(input.OwnerId);
        }

    }
}
