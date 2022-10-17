using Api_Aggregator.Contract.MediationDtos.MozaeekCore.BasicInfoDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api_Aggregator.ApplicationService.AggregationServices.UserAnnoucementBasicInfo
{
    public interface IAnnoucementUserDashboardBasicInfoAggregationService
    {
        Task<List<UserAnnouncementDto>> GetUserAnnoucement(long userId);

        Task<UserSearchResult> FullSearchByUserCharacteristics(int ownerId);
    }
}
