using Microsoft.EntityFrameworkCore;
using MozaeekUserProfile.Domain;
using MozaeekUserProfile.Domain.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace MozaeekUserProfile.Persistense.EF.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MozaeekUserProfileContext _dbContext;

        public UserRepository(MozaeekUserProfileContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<User> AddIfNotExist(User user)
        {
            var dbUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.PhoneNumber.Equals(user.PhoneNumber));
            if (dbUser != null)
            {
                return dbUser;
            }
            _dbContext.Add(user);
            return user;
        }

        public Task<User> GetUserById(long userId)
        {
            return _dbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<User> GetUserByMobileNo(string mobileNo)
        {
            var dbUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.PhoneNumber.Equals(mobileNo));
            if (dbUser != null)
            {
                return dbUser;
            }
            return null;
        }

        
        public  void UpdateProfile(User user)
        {
            _dbContext.Users.Update(user);
        }
    }
}
