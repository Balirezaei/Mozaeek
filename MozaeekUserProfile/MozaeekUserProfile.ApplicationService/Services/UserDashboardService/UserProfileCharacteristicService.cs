using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekUserProfile.ApplicationService.Contract.Dtos;
using MozaeekUserProfile.Common.ExtensionMethod;
using MozaeekUserProfile.Core.Core;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.ApplicationService.Services
{
    public interface IUserProfileCharacteristicService
    {
        Task<UserProfileCharacteristicOwnerCreateResult> CreateCharacteristicOwner(UserProfileCharacteristicOwnerInput input);
        Task<UserProfileCharacteristicCreateResult> CreateCharacteristic(UserProfileCharacteristicInput input);
        Task<UserProfileCharacteristicOwnerDeleteResult> DeleteUserProfileCharacteristicOwnerCreate(int id);
        Task<UserDashboardCharacteristicCreateResult> CreateUserDashboardCharacteristic(UserDashboardCharacteristicInput input);
        Task<List<UserProfileCharacteristicOwnerDto>> GetAllUserProfileCharacteristicOwner(long userId);

        Task<List<UserProfileCharacteristicSelectDto>> GetUserProfileCharacteristicDashboardSelect(long userId);
        Task<List<UserProfileCharacteristicDetail>> GetUserProfileCharacteristicByOwner(int ownerId);

        Task<UserProfileCharacteristicDetail> GetCharacteristicPreSavedInLabelGroup(UserProfileCharacteristicPreSavedInLabelGroupInput input);
        Task<UserProfileCharacteristicOwnerCreateResult> UpdateCharacteristicOwner(UserProfileCharacteristicOwnerUpdateInput input);
    }

    public class UserProfileCharacteristicService : IUserProfileCharacteristicService
    {
        private readonly IUserProfileCharacteristicRepository _characteristicRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserProfileCharacteristicService(IUserProfileCharacteristicRepository characteristicRepository, IUnitOfWork unitOfWork)
        {
            _characteristicRepository = characteristicRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserProfileCharacteristicOwnerCreateResult> CreateCharacteristicOwner(UserProfileCharacteristicOwnerInput input)
        {
            var domain = new UserProfileCharacteristicOwner(input.OwnerTitle, input.UserId);
            domain = await _characteristicRepository.CreateUserProfileCharacteristicOwner(domain);
            await _unitOfWork.CommitAsync();
            return new UserProfileCharacteristicOwnerCreateResult()
            {
                Id = domain.Id,
                OwnerTitle = domain.Name
            };
        }

        public async Task<UserProfileCharacteristicCreateResult> CreateCharacteristic(UserProfileCharacteristicInput input)
        {
            if (input.OwnerId == 0)
            {
                var ownerCreateResult = await CreateCharacteristicOwner(new UserProfileCharacteristicOwnerInput()
                { OwnerTitle = input.OwnerTitle, UserId = input.UserId });
                input.OwnerId = ownerCreateResult.Id;
            }

            var owner = await _characteristicRepository.FindCharacteristicOwner(input.OwnerId);
            if (owner == null || owner.UserId != input.UserId)
            {
                throw new System.Exception("اطلاعات مالک شناسه نادرست می باشد.");
            }

            if (owner != null && owner.Name != input.OwnerTitle.Recheck())
            {
                owner.UpdateOwnerTitle(input.OwnerTitle);
            }

            var preSaved = (await _characteristicRepository
                .GetAllUserProfileCharacteristicByPredicate(m =>
                    m.UserProfileCharacteristicOwnerId == owner.Id && m.FirstLabelParentId == input.FirstLabelParentId)).SingleOrDefault();

            if (preSaved != null)
            {
                _characteristicRepository.DeleteUserProfileCharacteristic(preSaved);
            }

            var domain = new UserProfileCharacteristic(owner, input.LabelId, input.LabelTitle, input.FirstLabelParentTitle, input.FirstLabelParentId);
            await _characteristicRepository.CreateUserProfileCharacteristic(domain);
            await _unitOfWork.CommitAsync();

            return new UserProfileCharacteristicCreateResult()
            {
                Id = domain.Id,
                OwnerTitle = owner.Name,
                UserProfileCharacteristicId = domain.Id,
                SelectedLabelId = domain.LabelId.ToString(),
                FirstNodeId = domain.FirstLabelParentId.ToString(),
                FirstLabelTitle = domain.FirstLabelParentTitle,
                SelectedLabelTitle = domain.LabelTitle

            };
        }

        public async Task<UserProfileCharacteristicOwnerDeleteResult> DeleteUserProfileCharacteristicOwnerCreate(int id)
        {
            await _characteristicRepository.DeleteUserProfileCharacteristicOwner(id);

            await _unitOfWork.CommitAsync();
            return new UserProfileCharacteristicOwnerDeleteResult();
        }

        public async Task<UserDashboardCharacteristicCreateResult> CreateUserDashboardCharacteristic(UserDashboardCharacteristicInput input)
        {
            var title = "";
            foreach (var item in input.CharacteristicIds)
            {
                var characteristic = await _characteristicRepository.FindCharacteristic(item);
                var domain = new UserDashboardCharacteristic(item, input.UserId,
                    characteristic.UserProfileCharacteristicOwner.Name);
                await _characteristicRepository.CreateUserDashboardCharacteristic(domain);
                title = domain.Title;
            }

            await _unitOfWork.CommitAsync();
            return new UserDashboardCharacteristicCreateResult()
            {
                UserDashboardCharacteristicTitle = title
            };
        }

        public async Task<List<UserProfileCharacteristicOwnerDto>> GetAllUserProfileCharacteristicOwner(long userId)
        {
            var list = await _characteristicRepository.GetAllUserProfileCharacteristicOwner(userId);
            return list.Select(m => new UserProfileCharacteristicOwnerDto
            {
                Id = m.Id,
                UserId = m.UserId,
                Name = m.Name
            }).ToList();
        }

        public async Task<List<UserProfileCharacteristicSelectDto>> GetUserProfileCharacteristicDashboardSelect(long userId)
        {
            var list = await _characteristicRepository.GetAllUserProfileCharacteristic(userId);

            return list.GroupBy(m => m.UserProfileCharacteristicOwner)
                  .Select(m => new UserProfileCharacteristicSelectDto
                  {
                      OwnerName = m.Key.Name,
                      UserProfileCharacteristicDetails = m.Select(z => new UserProfileCharacteristicDetail
                      {
                          UserProfileCharacteristicId = z.Id,
                          LabelParentTitle = z.FirstLabelParentTitle,
                          SelectedLabel = z.LabelTitle,
                          SelectedLabelId = z.LabelId
                      }).ToList()
                  }).ToList();
        }

        public async Task<List<UserProfileCharacteristicDetail>> GetUserProfileCharacteristicByOwner(int ownerId)
        {
            var list = await _characteristicRepository.GetAllUserProfileCharacteristicByOwner(ownerId);
            return list
                .Select(z => new UserProfileCharacteristicDetail
                {
                    FirstNodeId = z.FirstLabelParentId,
                    UserProfileCharacteristicId = z.Id,
                    LabelParentTitle = z.FirstLabelParentTitle,
                    SelectedLabel = z.LabelTitle,
                    SelectedLabelId = z.LabelId
                }).ToList();
        }

        public async Task<UserProfileCharacteristicDetail> GetCharacteristicPreSavedInLabelGroup(UserProfileCharacteristicPreSavedInLabelGroupInput input)
        {
            var characteristic = await _characteristicRepository
                .GetAllUserProfileCharacteristicByPredicate(m =>
                 m.UserProfileCharacteristicOwnerId == input.OwnerId && m.UserProfileCharacteristicOwner.UserId == input.UserId &&
                    m.FirstLabelParentId == input.FirstLabelNodeId);
            if (!characteristic.Any())
            {
                return null;
                //return new UserProfileCharacteristicDetail();
            }
            var preSelectedLabel = characteristic.Single();

            return new UserProfileCharacteristicDetail(preSelectedLabel.FirstLabelParentTitle, preSelectedLabel.LabelTitle, preSelectedLabel.Id, preSelectedLabel.LabelId, preSelectedLabel.FirstLabelParentId);
        }

        public async Task<UserProfileCharacteristicOwnerCreateResult> UpdateCharacteristicOwner(UserProfileCharacteristicOwnerUpdateInput input)
        {

            var owner = await _characteristicRepository.FindCharacteristicOwner(input.OwnerId);
            if (owner == null || owner.UserId != input.UserId)
            {
                throw new System.Exception("اطلاعات مالک شناسه نادرست می باشد.");
            }

            if (owner != null && owner.Name != input.OwnerTitle.Recheck())
            {
                owner.UpdateOwnerTitle(input.OwnerTitle);
                await _unitOfWork.CommitAsync();
            }


            return new UserProfileCharacteristicOwnerCreateResult()
            {
                Id = owner.Id,
                OwnerTitle = owner.Name
            };
        }
    }
}