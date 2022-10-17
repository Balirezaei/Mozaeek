using System;
using System.Threading.Tasks;
using Karmizban.Support.ApplicationService;
using Karmizban.Support.ApplicationService.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Karmizban.Support.RestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserRequestSupportController : ControllerBase
    {
        private readonly IUserRequestSupportService _userRequestSupportService;

        public UserRequestSupportController(IUserRequestSupportService userRequestSupportService)
        {
            _userRequestSupportService = userRequestSupportService;
        }

        [HttpPost]
        public Task<CreateUserRequestSupportResult> CreateRequest(CreateUserRequestSupportCommand cmd)
        {
            return _userRequestSupportService.Create(cmd);
        }

        [HttpGet]
        public string OcelotBug()
        {
            return $"{DateTime.Now}";
        }
        
    }
}