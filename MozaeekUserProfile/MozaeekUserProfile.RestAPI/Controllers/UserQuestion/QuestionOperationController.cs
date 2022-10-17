using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekUserProfile.ApplicationService.Contract;
using MozaeekUserProfile.ApplicationService.Services;

namespace MozaeekUserProfile.RestAPI.Controllers.UserQuestion
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionOperationController : ControllerBase
    {
        private readonly IUserQuestionService _userQuestionService;

        public QuestionOperationController(IUserQuestionService userQuestionService)
        {
            _userQuestionService = userQuestionService;
        }
        
        [HttpPost]
        public Task CancelQuestion(CancelQuestionInput input)
        {
            return _userQuestionService.CancelQuestion(input);
        }
    }
}
