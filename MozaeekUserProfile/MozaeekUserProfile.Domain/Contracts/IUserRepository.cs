using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekUserProfile.Domain.Contracts
{
    public interface IUserRepository
    {
        Task<User> AddIfNotExist(User user);
        void UpdateProfile(User user);
        Task<User> GetUserById(long userId);
        Task<User> GetUserByMobileNo(string mobileNo);

    }
}
