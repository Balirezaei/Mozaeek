using Api_Aggregator.Contract.MediationDtos.MozaeekCore.BasicInfoDtos;
using Api_Aggregator.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api_Aggregator.Contract.MediationDtos;
using Api_Aggregator.Infrastructure.ResponseMessages;
using Mozaeek.CR.PublicDto.Dto;

namespace Api_Aggregator.ApplicationService.MediationServices.MozaeekCore.BasicInfo
{
    public class BasicInfoMediationService : IBasicInfoMediationService
    {
        private readonly IHttpClientFactory _clientFactory;

        public BasicInfoMediationService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<UserAnnouncementDto>> GetUserDashboardAnnouncement(AnnouncementUserDashboardDto announcement)
        {
            var client = _clientFactory.CreateClient("MozaeekCore");
            var bsClient = new HttpClientRequestor(client);
            var result = await bsClient.RequestPostAsync<Result<PagedListResult<UserAnnouncementDto>>>(announcement, "BasicInfo/GetUserDashboardAnnouncement");

            return result.Data.List.ToList();
        }

        public async Task<PagedListResult<LabelGrid>> GetAllLabelChildrenWithParentID(long parentId)
        {
            var client = _clientFactory.CreateClient("MozaeekCore");
            var bsClient = new HttpClientRequestor(client);
            var result = await bsClient.RequestGetAsync<Result<PagedListResult<LabelGrid>>>("BasicInfo/GetAllChildrenLabel?id=" + parentId);
            return result.Data;
        }

        public async Task<List<LabelGrid>> GetAllParentLabel()
        {
            var client = _clientFactory.CreateClient("MozaeekCore");
            var bsClient = new HttpClientRequestor(client);
            var result = await bsClient.RequestGetAsync<Result<PagedListResult<LabelGrid>>>("BasicInfo/GetAllParentLabel?PageSize=25&PageNumber=1");
            return result.Data.List;
        }

        public async Task<UserSearchResult> FullSearchByUserCharacteristics(FullUserSearchByUserCharacteristics input)
        {
            var client = _clientFactory.CreateClient("MozaeekCore");
            var bsClient = new HttpClientRequestor(client);
            var result = await bsClient.RequestPostAsync<Result<UserSearchResult>>(input, "UserSearch/FullSearchByUserCharacteristics");
            return result.Data;
        }

        public async Task<SubjectWithPriceDetailDto> GetSubjectWithPriceDetail(long subjectId)
        {
            var client = _clientFactory.CreateClient("MozaeekCore");
            var bsClient = new HttpClientRequestor(client);
            var result = await bsClient.RequestGetAsync<Result<SubjectWithPriceDetailDto>>("BasicInfo/GetSubjectWithPriceDetail?subjectId=" + subjectId);
            return result.Data;
        }
    }
}
