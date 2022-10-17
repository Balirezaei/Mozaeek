using MozaeekCore.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.Domain.Identity
{
    public interface IUserRepository: IGenericRepository<User>
    {
        void AddNew(User user);
        Task<User> CheckAndGet(string username, string password);
        Task<User> GetUserByUserName(string username);
        Task ChangePassword(string username, string newPassword);
        Task UpdateUser(User user,long Id, List<UserRole> userRoles);
        Task<bool> CheckExistUser(string userName);
        Task AddRole(long userId, CoreRole role);
        Task RemoveRole(long userId, CoreRole role);
        Task RemoveRoles(long userId);
        Task UpdateUserTokenExpired(long userId,string expiredToken);
    }
}
