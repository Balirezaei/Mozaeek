using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mozaeek.CR.PublicDto;
using MozaeekUserProfile.ApplicationService.Contract;
using MozaeekUserProfile.ApplicationService.Services;

namespace MozaeekUserProfile.RestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionWorkflowController : ControllerBase
    {
        private readonly IQuestionWorkflowService _questionWorkflowService;

        public QuestionWorkflowController(IQuestionWorkflowService questionWorkflowService)
        {
            _questionWorkflowService = questionWorkflowService;
        }
        
        [HttpGet]
        public Task<List<UserQuestionForTechnicianProcess>> GetAllQuestionForTechnicianProcesses()
        {
            return _questionWorkflowService.GetAllQuestionForTechnicianProcesses();
        }

        [HttpPost]
        public Task<UpdateUserQuestionStateResult> UpdateQuestionState(UpdateUserQuestionStateInput input)
        {
            return _questionWorkflowService.UpdateQuestionState(input);
        }
    }
}
