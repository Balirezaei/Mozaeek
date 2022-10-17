using Api_Aggregator.ApplicationService.MediationServices.UserProfile.AnnoucementUserDashboard;
using Api_Aggregator.Contract.MediationDtos.MozaeekCore.BasicInfoDtos;
using Api_Aggregator.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api_Aggregator.Infrastructure.ResponseMessages;
using Mozaeek.CR.PublicDto.Dto;

namespace Api_Aggregator.ApplicationService.MediationServices.UserProfile.UserAnnouncement
{
    public class AnnoucementUserDashboardMediationService : IAnnoucementUserDashboardMediationService
    {
        private readonly IHttpClientFactory _clientFactory;

        public AnnoucementUserDashboardMediationService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<AnnouncementUserDashboardDto> GetUserAnnouncement(long userId)
        {
            var client = _clientFactory.CreateClient("UserProfile");
            var bsClient = new HttpClientRequestor(client);
            var result = await bsClient.RequestGetAsync<Result<AnnouncementUserDashboardDto>>("AnnoucementUserDashboard/Get?userId=" + userId);
            return result.Data;
        }
    }
}
