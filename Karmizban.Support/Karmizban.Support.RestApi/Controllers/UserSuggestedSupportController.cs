using System.Collections.Generic;
using System.Threading.Tasks;
using Karmizban.Support.ApplicationService;
using Karmizban.Support.ApplicationService.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Karmizban.Support.RestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserSuggestedSupportController : ControllerBase
    {
        private readonly IUserSuggestedSupportService _suggestedSupportService;

        public UserSuggestedSupportController(IUserSuggestedSupportService suggestedSupportService)
        {
            _suggestedSupportService = suggestedSupportService;
        }

        [HttpPost]
        public Task<CreateUserSuggestedSupportResult> Create([FromBody] CreateUserSuggestedSupportCommand cmd)
        {
            return _suggestedSupportService.Create(cmd);
        }

        [HttpGet]
        public Task<List<UserSuggestedSupportDto>> GetAll()
        {
            return _suggestedSupportService.GetAll();
        }
    }
}