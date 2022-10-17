using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mozaeek.CR.PublicDto.Dto;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Facade.Contract;
using MozaeekCore.Facade.Query;
using MozaeekCore.ViewModel;

namespace MozaeekCore.RestAPI.Controllers.BasicInfo
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class BasicInfoController : ControllerBase
    {
        private readonly ILabelQueryFacade _labelQueryFacade;
        private readonly ISubjectQueryFacade _subjectQueryFacade;
        private readonly IRequestOrgQueryFacade _requestOrgQueryFacade;
        private readonly IPointQueryFacade _pointQueryFacade;
        private readonly IRequestTargetQueryFacade _requestTargetQueryFacade;
        private readonly IAnnouncementQueryFacade _announcementQueryFacade;

        public BasicInfoController(ILabelQueryFacade labelQueryFacade, ISubjectQueryFacade subjectQueryFacade, IRequestOrgQueryFacade requestOrgQueryFacade, IPointQueryFacade pointQueryFacade, IRequestTargetQueryFacade requestTargetQueryFacade, IAnnouncementQueryFacade announcementQueryFacade)
        {
            _labelQueryFacade = labelQueryFacade;
            _subjectQueryFacade = subjectQueryFacade;
            _requestOrgQueryFacade = requestOrgQueryFacade;
            _pointQueryFacade = pointQueryFacade;
            _announcementQueryFacade = announcementQueryFacade;
            _requestTargetQueryFacade = requestTargetQueryFacade;
        }

        #region Label
        [HttpGet]
        public Task<PagedListResult<LabelGrid>> GetAllParentLabel([FromQuery] LabelFilterContract pagingContract)
        {
            return _labelQueryFacade.GetAllParentLabels(pagingContract);
        }
        [HttpGet]
        public Task<PagedListResult<LabelGrid>> GetAllChildrenLabel(long id)
        {
            return _labelQueryFacade.GetAllLabelChildren(id);
        }
        #endregion

        #region Subject
        [HttpGet]
        public Task<PagedListResult<SubjectGrid>> GetAllParentSubject([FromQuery] SubjectFilterContract pagingContract)
        {
            return _subjectQueryFacade.GetAllParentSubjects(pagingContract);
        }
        [HttpGet]
        public Task<PagedListResult<SubjectGrid>> GetAllChildrenSubject(long id)
        {
            return _subjectQueryFacade.GetAllSubjectChildren(id);
        }
        #endregion

        #region RequestOrg
        [HttpGet]
        public Task<PagedListResult<RequestOrgGrid>> GetAllParentRequestOrg([FromQuery] RequestOrgFilterContract pagingContract)
        {
            return _requestOrgQueryFacade.GetAllParentRequestOrgs(pagingContract);
        }
        [HttpGet]
        public Task<PagedListResult<RequestOrgGrid>> GetAllChildrenRequestOrg(long id)
        {
            return _requestOrgQueryFacade.GetAllRequestOrgChildren(id);
        }
        #endregion

        [HttpGet]
        public Task<PagedListResult<PointGrid>> GetAllCity([FromQuery] PointFilterDto pointFilter)
        {
            return _pointQueryFacade.GetAllCity(pointFilter);
        }

        #region Point
        [HttpGet]
        public Task<PagedListResult<PointGrid>> GetAllPointParent([FromQuery] PointFilterDto filterDto)
        {
            return _pointQueryFacade.GetAllParentPoints(filterDto);
        }
        [HttpGet]
        public Task<PagedListResult<PointGrid>> GetAllPointChildren(long id)
        {
            return _pointQueryFacade.GetAllPointChildren(id);
        }
        #endregion

        [HttpGet]
        public Task<PagedListResult<RequestTargetMobileView>> GetAllRequestTarget([FromQuery] RequestTargetFilterDto pagingContract)
        {
            return _requestTargetQueryFacade.GetAllRequestTargetMobileView(pagingContract);
 
        }

        [HttpPost]
        public async Task<PagedListResult<UserAnnouncementDto>> GetUserDashboardAnnouncement(AnnouncementUserDashboardDto announcement)
        {
            var result = await _announcementQueryFacade.GetUserDashboardAnnouncement(announcement);
            return result;
        }

        [HttpGet]
        public Task<SubjectWithPriceDetailDto> GetSubjectWithPriceDetail(long subjectId)
        {
            return _subjectQueryFacade.GetSubjectWithPriceDetail(subjectId);
        }
        [HttpGet]
        public async Task<PagedListResult<DefiniteRequestOrgDto>> GetAllDefiniteeeRequestOrgByPointId(long pointId)
        {
            return await _requestOrgQueryFacade.GetAllDefiniteRequestOrdByPointId(pointId);
        }

    }
}
