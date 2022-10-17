using System.Threading.Tasks;
using Karmizban.Support.ApplicationService;
using Karmizban.Support.ApplicationService.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Karmizban.Support.RestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserRequestSupportAnswerController : ControllerBase
    {
        private readonly IUserRequestSupportService _userRequestSupportService;

        public UserRequestSupportAnswerController(IUserRequestSupportService userRequestSupportService)
        {
            _userRequestSupportService = userRequestSupportService;
        }

        [HttpPost]
        public Task<UserRequestSupportAnswerResult> Answer([FromBody] UserRequestSupportAnswerCommand cmd)
        {
            return _userRequestSupportService.Answer(cmd);
        }

    }
}