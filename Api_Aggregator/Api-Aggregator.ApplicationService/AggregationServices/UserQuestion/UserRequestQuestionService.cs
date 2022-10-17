using System;
using System.Net.Http;
using System.Threading.Tasks;
using Api_Aggregator.ApplicationService.MediationServices.MozaeekCore.PriceInquery;
using Api_Aggregator.ApplicationService.MediationServices.UserProfile.UserQuestion;
using Api_Aggregator.Contract.MediationDtos;
using Api_Aggregator.Contract.MediationDtos.Aggregator;
using Mozaeek.CR.PublicDto;
using Mozaeek.CR.PublicDto.Dto;

namespace Api_Aggregator.ApplicationService.AggregationServices.UserQuestion
{
    public interface IUserRequestQuestionService
    {
        Task<CreateQuestionResult> RegisterTextRequest(RegisterTextRequestQuestionDto dto);
        Task<CreateQuestionResult> RegisterVoiceRequest(RegisterVoiceRequestQuestionDto dto);
        Task<CreateQuestionResult> RegisterTextSubject(RegisterTextSubjectQuestionDto dto);
        Task<CreateQuestionResult> RegisterVoiceSubject(RegisterVoiceSubjectQuestionDto dto);
    }

    public class UserRequestQuestionService : IUserRequestQuestionService
    {

        private readonly IPriceInqueryMediationService _priceInqueryMediationService;
        private readonly IUserQuestionMedationService _userQuestionMedationService;

        public UserRequestQuestionService(IPriceInqueryMediationService priceInqueryMediationService, IUserQuestionMedationService userQuestionMedationService)
        {
            _priceInqueryMediationService = priceInqueryMediationService;
            _userQuestionMedationService = userQuestionMedationService;
        }

        public async Task<CreateQuestionResult> RegisterTextRequest(RegisterTextRequestQuestionDto dto)
        {
            var properPrice = await _priceInqueryMediationService.GetRequestQuestionPrice(dto.RequestId);
            if (properPrice == null)
            {
                throw new Exception("قیمت تعریف نشده است");
            }
            var questDto = new CreateTextRequestQuestionDto(dto.UserId, dto.QuestionTitle, dto.Description, dto.RequestId, properPrice.ProperPriceResult, dto.IsTextAnswer, properPrice.RequestTitle, properPrice.TechnicianType, dto.Attachments);
            return await _userQuestionMedationService.CreateTextRequestQuestion(questDto);
        }
        public async Task<CreateQuestionResult> RegisterVoiceRequest(RegisterVoiceRequestQuestionDto dto)
        {
            var properPrice = await _priceInqueryMediationService.GetRequestQuestionPrice(dto.RequestId);
            if (properPrice == null)
            {
                throw new Exception("قیمت تعریف نشده است");
            }

            var questDto = new CreateVoiceRequestQuestionDto(dto.UserId, "", dto.RequestId, properPrice.ProperPriceResult, dto.IsTextAnswer, properPrice.RequestTitle, properPrice.TechnicianType, dto.Voice, dto.Attachments);

            return await _userQuestionMedationService.CreateVoiceRequestQuestion(questDto);
        }

        public async Task<CreateQuestionResult> RegisterVoiceSubject(RegisterVoiceSubjectQuestionDto dto)
        {
            var properPrice = await _priceInqueryMediationService.GetSubjectQuestionPrice(dto.SubjectId);
            if (properPrice == null)
            {
                throw new Exception("قیمت تعریف نشده است");
            }

            var questDto = new CreateVoiceSubjectQuestionDto(dto.UserId, dto.QuestionTitle, dto.SubjectId, properPrice.ProperPriceResult, dto.IsTextAnswer, properPrice.SubjectTitle, properPrice.TechnicianType, dto.Voice, dto.Attachments);

            return await _userQuestionMedationService.CreateVoiceSubjectQuestion(questDto);
        }

        public async Task<CreateQuestionResult> RegisterTextSubject(RegisterTextSubjectQuestionDto dto)
        {
            var properPrice = await _priceInqueryMediationService.GetSubjectQuestionPrice(dto.SubjectId);
            if (properPrice == null)
            {
               throw new Exception("قیمت تعریف نشده است");
            }

            var questDto = new CreateTextSubjectQuestionDto(dto.UserId, dto.QuestionTitle, dto.Description, dto.SubjectId, properPrice.ProperPriceResult, dto.IsTextAnswer, properPrice.SubjectTitle, properPrice.TechnicianType, dto.Attachments);

            return await _userQuestionMedationService.CreateTextSubjectQuestion(questDto);
        }
    }


}