using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Mozaeek.CR.PublicDto;
using Mozaeek.CR.PublicDto.Dto;
using MozaeekUserProfile.ApplicationService.Contract;
using MozaeekUserProfile.ApplicationService.Services;

namespace MozaeekUserProfile.RestAPI.Controllers.UserQuestion
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IUserQuestionService _userQuestionService;
        private readonly IUserQuestionQueryService _questionQueryService;

        public QuestionController(IUserQuestionService userQuestionService, IUserQuestionQueryService questionQueryService)
        {
            _userQuestionService = userQuestionService;
            _questionQueryService = questionQueryService;
        }

        [HttpPost]
        public Task<CreateQuestionResult> CreateTextRequestQuestion([FromBody] CreateTextRequestQuestionDto dto)
        {
            return _userQuestionService.CreateTextRequestQuestion(dto);
        }

        [HttpPost]
        public Task<CreateQuestionResult> CreateVoiceRequestQuestion([FromBody] CreateVoiceRequestQuestionDto dto)
        {
            return _userQuestionService.CreateVoiceRequestQuestion(dto);
        }

        [HttpPost]
        public Task<CreateQuestionResult> CreateTextSubjectQuestion([FromBody] CreateTextSubjectQuestionDto dto)
        {
            return _userQuestionService.CreateTextSubjectQuestion(dto);
        }
        [HttpPost]
        public Task<CreateQuestionResult> CreateVoiceSubjectQuestion([FromBody] CreateVoiceSubjectQuestionDto dto)
        {
            return _userQuestionService.CreateVoiceSubjectQuestion(dto);
        }

        [HttpGet]
        public Task<List<UserQuestionHistory>> GetAllUserQuestion([FromQuery] UserQuestionHistoryQuery query)
        {
            return _questionQueryService.GetAllUserQuestion(query);
        }

        [HttpGet]
        public Task<List<UserQuestionHistory>> GetRecentUserQuestion([FromQuery] UserQuestionHistoryQuery query)
        {
            return _questionQueryService.GetRecentUserQuestion(query);
        }

        [HttpGet]
        public Task<ActiveUserQuestion> GetActiveUserQuestion(long userId)
        {
            return _questionQueryService.GetActiveUserQuestion(userId);
        }

        [HttpGet]
        public Task<OpenUserQuestion> GetOpenUserQuestion(long userId)
        {
            return _questionQueryService.GetOpenUserQuestion(userId);
        }
    }
}
