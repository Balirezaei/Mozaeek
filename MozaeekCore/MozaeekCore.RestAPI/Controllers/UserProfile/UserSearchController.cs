using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Facade.Query.UserProfile;
using MozaeekCore.ViewModel;
using MozaeekCore.ApplicationService.Contract.UserProfile;

namespace MozaeekCore.RestAPI.Controllers.UserProfile
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserSearchController : ControllerBase
    {
        private readonly IUserSearchQueryFacade _userSearchQueryFacade;

        public UserSearchController(IUserSearchQueryFacade userSearchQueryFacade)
        {
            _userSearchQueryFacade = userSearchQueryFacade;
        }
        [HttpGet]
        public Task<SingleUserAnnouncementDto> GetAnnouncementById(long id)
        {
            return _userSearchQueryFacade.GetUserAnnouncementById(id);
        }

        [HttpGet]
        public Task<SingleUserRequestDto> GetRequestByRequestTargetId(long requestTargetId)
        {
            return _userSearchQueryFacade.GetUserRequestByRequestTargetId(requestTargetId);
        }

        [HttpGet]
        public Task<SingleUserRequestDto> GetRequestByTargetIdRequestAct(long requestTargetId, long requestActId,long pointId)
        {
            return _userSearchQueryFacade.GetUserRequestByRequestTargetId(requestTargetId, requestActId,pointId);
        }
        [HttpPost]
        public async Task<UserSearchResult> FullTextSearch([FromBody] FullTextSearchDto input)
        {
            return await _userSearchQueryFacade.FullTextSearch(input);
        }
        
        [HttpPost]
        public async Task<UserSearchResult> FullSearchBySubject([FromBody] FullUserSearchBySubject input)
        {
            return await _userSearchQueryFacade.FullUserSearchBySubject(input);
        }


        [HttpPost]
        public async Task<UserSearchResult> FullSearchByUserCharacteristics([FromBody] FullUserSearchByUserCharacteristics input)
        {
            return await _userSearchQueryFacade.FullSearchByUserCharacteristics(input);
        }


        [HttpPost]
        public async Task<UserSearchResult> FullSearchByRequestTarget([FromBody] FullUserSearchByRequestTarget input)
        {
            return await _userSearchQueryFacade.FullUserSearchByRequestTarget(input);
        }

        [HttpPost]
        public async Task<UserSearchResult> FullSearchByRequestOrg([FromBody] FullUserSearchByRequestOrg input)
        {
            return await _userSearchQueryFacade.FullUserSearchByRequestOrg(input);
        }
    }
}
