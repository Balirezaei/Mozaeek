using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Aggregator.ApplicationService.AggregationServices.UserQuestion;
using Api_Aggregator.Contract.MediationDtos;
using Api_Aggregator.Contract.MediationDtos.Aggregator;

namespace Api_Aggregator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegisterQuestionController : ControllerBase
    {
        private readonly IUserRequestQuestionService _userRequestQuestionService;

        public RegisterQuestionController(IUserRequestQuestionService userRequestQuestionService)
        {
            _userRequestQuestionService = userRequestQuestionService;
        }

        [HttpPost]
        public Task<CreateQuestionResult> RegisterTextRequestQuestion(RegisterTextRequestQuestionDto dto)
        {
            return _userRequestQuestionService.RegisterTextRequest(dto);
        }


        [HttpPost]
        public Task<CreateQuestionResult> RegisterVoiceRequestQuestion(RegisterVoiceRequestQuestionDto dto)
        {
            return _userRequestQuestionService.RegisterVoiceRequest(dto);
        }

        [HttpPost]
        public Task<CreateQuestionResult> RegisterTextSubjectQuestion(RegisterTextSubjectQuestionDto dto)
        {
            return _userRequestQuestionService.RegisterTextSubject(dto);
        }

        [HttpPost]
        public Task<CreateQuestionResult> RegisterVoiceSubjectQuestion(RegisterVoiceSubjectQuestionDto dto)
        {
            return _userRequestQuestionService.RegisterVoiceSubject(dto);
        }
    }
}
