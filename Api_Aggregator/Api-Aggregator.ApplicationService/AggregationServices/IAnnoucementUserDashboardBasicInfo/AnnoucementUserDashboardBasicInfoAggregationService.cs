using Api_Aggregator.ApplicationService.MediationServices.MozaeekCore.BasicInfo;
using Api_Aggregator.ApplicationService.MediationServices.UserProfile.AnnoucementUserDashboard;
using Api_Aggregator.Contract.MediationDtos.MozaeekCore.BasicInfoDtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Aggregator.ApplicationService.MediationServices.UserProfile.Characteristic;

namespace Api_Aggregator.ApplicationService.AggregationServices.UserAnnoucementBasicInfo
{
    public class AnnoucementUserDashboardBasicInfoAggregationService : IAnnoucementUserDashboardBasicInfoAggregationService
    {
        private readonly IAnnoucementUserDashboardMediationService _userAnnouncementService;
        private readonly IBasicInfoMediationService _basicInfoService;
        private readonly ICharachterisiticMediationService _charachterisiticMediationService;

        public AnnoucementUserDashboardBasicInfoAggregationService(IAnnoucementUserDashboardMediationService userAnnouncementService, IBasicInfoMediationService basicInfoService, ICharachterisiticMediationService charachterisiticMediationService)
        {
            _userAnnouncementService = userAnnouncementService;
            _basicInfoService = basicInfoService;
            _charachterisiticMediationService = charachterisiticMediationService;
        }

        public async Task<List<UserAnnouncementDto>> GetUserAnnoucement(long userId)
        {
          var userAnnoucement= await _userAnnouncementService.GetUserAnnouncement(userId);
            return await _basicInfoService.GetUserDashboardAnnouncement(userAnnoucement);

        }

        public async Task<UserSearchResult> FullSearchByUserCharacteristics(int ownerId)
        {
            var selectedLabels =await _charachterisiticMediationService.CharacteristicUserDashboardDto(ownerId);
            var input = new FullUserSearchByUserCharacteristics
            {
                LabelIds = selectedLabels.Select(m => m.SelectedLabelId).ToList()
            };
            return await _basicInfoService.FullSearchByUserCharacteristics(input);
        }
    }
}
