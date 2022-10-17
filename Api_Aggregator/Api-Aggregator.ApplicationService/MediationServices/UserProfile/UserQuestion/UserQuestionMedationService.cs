using System;
using System.Net.Http;
using System.Threading.Tasks;
using Api_Aggregator.Contract.MediationDtos;
using Api_Aggregator.Infrastructure;
using Api_Aggregator.Infrastructure.ResponseMessages;
using Mozaeek.CR.PublicDto;
using Mozaeek.CR.PublicDto.Dto;

namespace Api_Aggregator.ApplicationService.MediationServices.UserProfile.UserQuestion
{
    public interface IUserQuestionMedationService
    {
        Task<CreateQuestionResult> CreateTextRequestQuestion(CreateTextRequestQuestionDto dto);
        Task<CreateQuestionResult> CreateVoiceRequestQuestion(CreateVoiceRequestQuestionDto dto);
        Task<CreateQuestionResult> CreateTextSubjectQuestion(CreateTextSubjectQuestionDto dto);
        Task<CreateQuestionResult> CreateVoiceSubjectQuestion(CreateVoiceSubjectQuestionDto dto);
    }

    public class UserQuestionMedationService : IUserQuestionMedationService
    {
        private readonly IHttpClientFactory _clientFactory;
        public UserQuestionMedationService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<CreateQuestionResult> CreateTextRequestQuestion(CreateTextRequestQuestionDto dto)
        {
            var client = _clientFactory.CreateClient("UserProfile");
            var bsClient = new HttpClientRequestor(client);
            var result = await bsClient.RequestPostAsync<Result<CreateQuestionResult>>(dto, "Question/CreateTextRequestQuestion");
            if (result.Error != null)
            {
                throw new Exception(result.Error.Message);
            }
            return result.Data;
        }

        public async Task<CreateQuestionResult> CreateVoiceRequestQuestion(CreateVoiceRequestQuestionDto dto)
        {
            var client = _clientFactory.CreateClient("UserProfile");
            var bsClient = new HttpClientRequestor(client);
            var result = await bsClient.RequestPostAsync<Result<CreateQuestionResult>>(dto, "Question/CreateVoiceRequestQuestion");
            if (result.Error != null)
            {
                throw new Exception(result.Error.Message);
            }
            return result.Data;
        }

        public async Task<CreateQuestionResult> CreateVoiceSubjectQuestion(CreateVoiceSubjectQuestionDto dto)
        {
            var client = _clientFactory.CreateClient("UserProfile");
            var bsClient = new HttpClientRequestor(client);
            var result = await bsClient.RequestPostAsync<Result<CreateQuestionResult>>(dto, "Question/CreateVoiceSubjectQuestion");
            if (result.Error != null)
            {
                throw new Exception(result.Error.Message);
            }
            return result.Data;
        }

        public async Task<CreateQuestionResult> CreateTextSubjectQuestion(CreateTextSubjectQuestionDto dto)
        {
            var client = _clientFactory.CreateClient("UserProfile");
            var bsClient = new HttpClientRequestor(client);
            var result = await bsClient.RequestPostAsync<Result<CreateQuestionResult>>(dto, "Question/CreateTextSubjectQuestion");
            if (result.Error != null)
            {
                throw new Exception(result.Error.Message);
            }
            return result.Data;
        }
    }
}