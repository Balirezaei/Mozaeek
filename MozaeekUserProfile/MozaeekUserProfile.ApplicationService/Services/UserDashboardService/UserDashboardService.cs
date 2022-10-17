using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekUserProfile.ApplicationService.Contract.Dtos;
using MozaeekUserProfile.Common;
using MozaeekUserProfile.Core.Core;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.ApplicationService.Services.UserDashboardService
{
    public interface IUserDashboardService
    {
        Task<UserDashboardCreateResult> CreateUserDashboard(UserDashboardInputDto dto);
        Task<List<UserDashboardDto>> GetAllUserDashboard(long userId);
        Task<List<UserDashboardDto>> GetAllWorkbenchUserDashboardWithCharactreristic(long userId);
        Task RemoveUserDashboard(long userDashboardId);
        Task RemoveUserDashboardCharacteristic(int ownerId);
        Task<List<CharacteristicUserDashboardDto>> GetUserDashboardCharacteristic(int ownerId);
    }
    public class UserDashboardService : IUserDashboardService
    {
        private readonly IUserDashboardRepository _userDashboardRepository;
        private readonly IUserProfileCharacteristicRepository _characteristicRepository;

        private readonly IUnitOfWork _unitOfWork;

        public UserDashboardService(IUserDashboardRepository userDashboardRepository, IUnitOfWork unitOfWork, IUserProfileCharacteristicRepository characteristicRepository)
        {
            _userDashboardRepository = userDashboardRepository;
            _unitOfWork = unitOfWork;
            _characteristicRepository = characteristicRepository;
        }

        public async Task<UserDashboardCreateResult> CreateUserDashboard(UserDashboardInputDto dto)
        {
            var domain = new UserDashboard(0, dto.EntityType, dto.UserId, dto.Title, dto.EntityId);
            await _userDashboardRepository.CreateDashboard(domain);
            await _unitOfWork.CommitAsync();
            return new UserDashboardCreateResult
            {
                EntityType = domain.EntityType,
                Title = domain.EntityTypeDescription,
                //DashboardType = domain.DashboardType,
                Id = domain.Id
            };
        }

        public async Task<List<UserDashboardDto>> GetAllUserDashboard(long userId)
        {
            var list = await _userDashboardRepository.GetAll(userId);
            return list.Select(m => new UserDashboardDto()
            {
                UserId = m.UserId,
                EntityId = m.EntityId,
                EntityType = m.EntityType,
                EntityTypeDescription = m.EntityTypeDescription,
                Title = m.Title,
                Id = m.Id
            }).ToList();
        }

        public async Task<List<UserDashboardDto>> GetAllWorkbenchUserDashboardWithCharactreristic(long userId)
        {
            var list = await _userDashboardRepository.GetAll(userId);
            var characteristics = await _characteristicRepository.GetAllUserDashboardCharacteristic(userId).ToListAsync();

            var dashboardByCharacteristic = characteristics.GroupBy(m => m.Title)
                  .Select(m => new UserDashboardDto
                  {
                      UserId = userId,
                      EntityId = m.First().UserProfileCharacteristic.UserProfileCharacteristicOwnerId,
                      EntityType = EntityType.CharacteristicOwner,
                      EntityTypeDescription = "Characteristic",
                      Title = m.First().Title,
                      Id = m.First().UserProfileCharacteristic.UserProfileCharacteristicOwnerId
                  }).ToList();

            dashboardByCharacteristic.AddRange(
                list.Select(m => new UserDashboardDto()
                {
                    UserId = m.UserId,
                    EntityId = m.EntityId,
                    EntityType = m.EntityType,
                    EntityTypeDescription = m.EntityTypeDescription,
                    Title = m.Title,
                    Id = m.Id
                }));
            return dashboardByCharacteristic;
        }

        public async Task RemoveUserDashboard(long userDashboardId)
        {
            await _userDashboardRepository.Remove(userDashboardId);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveUserDashboardCharacteristic(int ownerId)
        {
            await _characteristicRepository.RemoveUserDashboardCharacteristic(ownerId);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<CharacteristicUserDashboardDto>> GetUserDashboardCharacteristic(int ownerId)
        {
            var result = await _characteristicRepository.GetAllUserDashboardCharacteristicByOwner(ownerId);
            return result.Select(m => new CharacteristicUserDashboardDto
            {
                SelectedLabelTitle = m.UserProfileCharacteristic.LabelTitle,
                ParentLabelId = m.UserProfileCharacteristic.FirstLabelParentId,
                SelectedLabelId = m.UserProfileCharacteristic.LabelId
            }).ToList();
        }
    }
}