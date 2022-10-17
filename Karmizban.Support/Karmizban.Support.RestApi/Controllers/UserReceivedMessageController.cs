using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karmizban.Support.ApplicationService;
using Karmizban.Support.ApplicationService.Contract;

namespace Karmizban.Support.RestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserReceivedMessageController : ControllerBase
    {
        private readonly IUserReceivedMessageService _userReceivedMessageService;

        public UserReceivedMessageController(IUserReceivedMessageService userReceivedMessageService)
        {
            _userReceivedMessageService = userReceivedMessageService;
        }

        [HttpGet]
        public Task<List<UserReceivedMessageDto>> GetAll([FromQuery] GetUserReceivedMessageContract contract)
        {
            return _userReceivedMessageService.GetAll(contract);
        }

        [HttpGet]
        public Task<UserRequestSupportAnswerDetail> GetAnswerDetail([FromQuery] GetUserRequestSupportAnswerContract contract)
        {
            return _userReceivedMessageService.GetAnswerDetail(contract);
        }
    }
}
