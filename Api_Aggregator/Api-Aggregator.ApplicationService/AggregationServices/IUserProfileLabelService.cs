using System.Linq;
using System.Threading.Tasks;
using Api_Aggregator.ApplicationService.MediationServices.MozaeekCore.BasicInfo;
using Api_Aggregator.ApplicationService.MediationServices.UserProfile.Characteristic;
using Api_Aggregator.Contract.MediationDtos;
using Api_Aggregator.Contract.MediationDtos.MozaeekCore.BasicInfoDtos;
using Api_Aggregator.Contract.MediationDtos.MozaeekCore.Common;
using Api_Aggregator.Infrastructure.ResponseMessages;

namespace Api_Aggregator.ApplicationService.AggregationServices
{
    public interface IUserProfileLabelService
    {
        Task<PagedListResult<LabelGrid>> GetAllLabelChildren(GetAllChildrenLabelInput input);
        Task<UnionUserProfileCharacteristicSelectAndUnSelectedDto> GetUnionUserProfileCharacteristicSelectAndUnSelectedDto(int ownerId);
    }
    public class UserProfileLabelService : IUserProfileLabelService
    {
        private readonly IBasicInfoMediationService _basicInfoService;
        private readonly ICharachterisiticMediationService _charachterisiticMediationService;
        public UserProfileLabelService(IBasicInfoMediationService basicInfoService, ICharachterisiticMediationService charachterisiticMediationService)
        {
            _basicInfoService = basicInfoService;
            _charachterisiticMediationService = charachterisiticMediationService;
        }

        public async Task<PagedListResult<LabelGrid>> GetAllLabelChildren(GetAllChildrenLabelInput input)
        {
            var labelChildren = await _basicInfoService.GetAllLabelChildrenWithParentID(input.ParentId);
            if (labelChildren.List.Any(m => m.HasChild == false))
            {
                var preSaved = await _charachterisiticMediationService.GetCharacteristicPreSavedInLabelGroup(
                    new UserProfileCharacteristicPreSavedInLabelGroupInput()
                    {
                        FirstLabelNodeId = input.FirstNodeId,
                        UserId = input.UserId,
                        OwnerId = input.OwnerId
                    });
                if (preSaved != null)
                {
                    foreach (var label in labelChildren.List)
                    {
                        if (label.Id == preSaved.SelectedLabelId)
                        {
                            label.Selected = true;
                        }
                    }
                }
            }
            return labelChildren;
        }

        public async Task<UnionUserProfileCharacteristicSelectAndUnSelectedDto> GetUnionUserProfileCharacteristicSelectAndUnSelectedDto(int ownerId)
        {
            var preSaved = await _charachterisiticMediationService.GetCharacteristicPreSavedByOwner(ownerId);
            var allParent = await _basicInfoService.GetAllParentLabel();

            var notExist =
                allParent.Where(m => preSaved.All(z => z.FirstNodeId != m.Id)).ToList();

            return new UnionUserProfileCharacteristicSelectAndUnSelectedDto()
            {
                UserProfileCharacteristicDetails = preSaved,
                UnSelectedLabel = notExist
            };
        }
    }
}