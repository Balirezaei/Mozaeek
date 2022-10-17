using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Api_Aggregator.Contract.MediationDtos;
using Api_Aggregator.Contract.MediationDtos.MozaeekCore.BasicInfoDtos;
using Api_Aggregator.Infrastructure;
using Api_Aggregator.Infrastructure.ResponseMessages;

namespace Api_Aggregator.ApplicationService.MediationServices.UserProfile.Characteristic
{
    public interface ICharachterisiticMediationService
    {
        Task<UserProfileCharacteristicDetail> GetCharacteristicPreSavedInLabelGroup(UserProfileCharacteristicPreSavedInLabelGroupInput input);
        Task<List<UserProfileCharacteristicDetail>> GetCharacteristicPreSavedByOwner(int ownerId);
        Task<List<CharacteristicUserDashboardDto>> CharacteristicUserDashboardDto(int ownerId);
    }
    public class CharachterisiticMediationService: ICharachterisiticMediationService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CharachterisiticMediationService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<UserProfileCharacteristicDetail> GetCharacteristicPreSavedInLabelGroup(UserProfileCharacteristicPreSavedInLabelGroupInput input)
        {
            var client = _clientFactory.CreateClient("UserProfile");
            var bsClient = new HttpClientRequestor(client);
            var result = await bsClient.RequestPostAsync<Result<UserProfileCharacteristicDetail>>(input,"UserProfileCharacteristic/GetCharacteristicPreSavedInLabelGroup");
            return result.Data;
        }

        public async Task<List<UserProfileCharacteristicDetail>> GetCharacteristicPreSavedByOwner(int ownerId)
        {
            var client = _clientFactory.CreateClient("UserProfile");
            var bsClient = new HttpClientRequestor(client);
            var result = await bsClient.RequestGetAsync<Result<List<UserProfileCharacteristicDetail>>>("UserProfileCharacteristic/GetAllUserProfileCharacteristicByOwner?ownerId="+ownerId);
            return result.Data;
        }

        public async Task<List<CharacteristicUserDashboardDto>> CharacteristicUserDashboardDto(int ownerId)
        {
            var client = _clientFactory.CreateClient("UserProfile");
            var bsClient = new HttpClientRequestor(client);
            var result = await bsClient.RequestGetAsync<Result<List<CharacteristicUserDashboardDto>>>("UserDashboard/GetUserDashboardCharacteristic?ownerId=" + ownerId);
            return result.Data;
        }
    }
}