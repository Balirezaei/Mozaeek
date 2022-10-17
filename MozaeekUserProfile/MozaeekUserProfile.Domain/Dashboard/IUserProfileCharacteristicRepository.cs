using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MozaeekUserProfile.Domain
{
    public interface IUserProfileCharacteristicRepository
    {
        Task<UserProfileCharacteristicOwner> CreateUserProfileCharacteristicOwner(UserProfileCharacteristicOwner domain);
        Task DeleteUserProfileCharacteristicOwner(int id);
        void DeleteUserProfileCharacteristic(UserProfileCharacteristic domain);
        Task CreateUserProfileCharacteristic(UserProfileCharacteristic domain);
        Task<UserDashboardCharacteristic> CreateUserDashboardCharacteristic(UserDashboardCharacteristic domain);
        Task<UserProfileCharacteristic> FindCharacteristic(int inputCharacteristicId);
        Task<List<UserProfileCharacteristicOwner>> GetAllUserProfileCharacteristicOwner(long userId);
        Task<List<UserProfileCharacteristic>> GetAllUserProfileCharacteristic(long userId);
        Task<List<UserProfileCharacteristic>> GetAllUserProfileCharacteristicByOwner(int ownerId);
        Task<List<UserProfileCharacteristic>> GetAllUserProfileCharacteristicByPredicate(Expression<Func<UserProfileCharacteristic, bool>> predicate);
        Task<UserProfileCharacteristicOwner> FindCharacteristicOwner(int ownerId);
        IQueryable<UserDashboardCharacteristic> GetAllUserDashboardCharacteristic(long userId);
        Task RemoveUserDashboardCharacteristic(int ownerId);
        Task<List<UserDashboardCharacteristic>> GetAllUserDashboardCharacteristicByOwner(int ownerId);
    }
}