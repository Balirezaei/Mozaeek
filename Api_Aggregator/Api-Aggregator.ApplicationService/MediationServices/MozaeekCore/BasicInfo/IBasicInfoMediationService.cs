using Api_Aggregator.Contract.MediationDtos.MozaeekCore.BasicInfoDtos;
using Mozaeek.CR.PublicDto.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api_Aggregator.Contract.MediationDtos;

namespace Api_Aggregator.ApplicationService.MediationServices.MozaeekCore.BasicInfo
{
    public interface IBasicInfoMediationService
    {
        Task<List<UserAnnouncementDto>> GetUserDashboardAnnouncement(AnnouncementUserDashboardDto announcement);
        Task<Infrastructure.ResponseMessages.PagedListResult<LabelGrid>> GetAllLabelChildrenWithParentID(long parentId);
        Task<List<LabelGrid>> GetAllParentLabel();
        Task<UserSearchResult> FullSearchByUserCharacteristics(FullUserSearchByUserCharacteristics input);
        Task<SubjectWithPriceDetailDto> GetSubjectWithPriceDetail(long subjectId);

    }
}
