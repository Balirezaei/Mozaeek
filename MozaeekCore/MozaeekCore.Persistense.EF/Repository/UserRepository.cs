using MozaeekCore.Common.Crypto;
using MozaeekCore.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.Exceptions;
using MozaeekCore.Enum;

namespace MozaeekCore.Persistense.EF.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly CoreDomainContext context;
        private readonly IPasswordService passwordService;

        public UserRepository(CoreDomainContext context,
                              IPasswordService passwordService)
            : base(context)
        {
            this.context = context;
            this.passwordService = passwordService;
        }
        public void AddNew(User user)
        {
            var hashedPasswordResult = passwordService.GenerateHashPassword(user.Password);
            user.Password = hashedPasswordResult.hash;
            user.Salt = hashedPasswordResult.salt;
            context.Users.Add(user);
        }

        public async Task AddRole(long userId, CoreRole role)
        {
            var dbUser = await context.Users.AnyAsync(x => x.Id == userId);
            if (dbUser == false)
            {
                throw new UserFriendlyException("User Not Found!");
            }
            var userRole = await context.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId && x.Role == role);
            if (userRole != null)
            {
                return;
            }
            UserRole newRole = new UserRole(role, userId);
            context.UserRoles.Add(newRole);
            await context.SaveChangesAsync();
        }

        public async Task ChangePassword(string username, string newPassword)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName.Equals(username));
            if (user == null)
            {
                throw new UserFriendlyException("User Not Found!");
            }
            var hashedPasswordResult = passwordService.GenerateHashPassword(newPassword);
            user.Password = hashedPasswordResult.hash;
            user.Salt = hashedPasswordResult.salt;
            await context.SaveChangesAsync();
        }

        public async Task<User> CheckAndGet(string username, string password)
        {
            var user = await context.Users.Where(x => x.UserName.Equals(username)).Include(x => x.UserRoles).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new UserFriendlyException("اطلاعات کاربری صحیح نیست!");
            }
            var verifyResult = passwordService.Verify(password, user.Password, user.Salt);
            if (!verifyResult)
            {
                throw new UserFriendlyException("اطلاعات کاربری صحیح نیست!");
            }
            return user;
        }
        public async Task<User> GetUserByUserName(string username)
        {
            var user = await context.Users.Where(x => x.UserName.Equals(username)).Include(x => x.UserRoles).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new UserFriendlyException("User Not Found!");
            }
            return user;
        }

        public Task<bool> CheckExistUser(string userName)
        {
            return context.Users.AnyAsync(x => x.UserName.Equals(userName));
        }

        public async Task RemoveRole(long userId, CoreRole role)
        {
            var dbUser = await context.Users.AnyAsync(x => x.Id == userId);
            if (dbUser == false)
            {
                throw new UserFriendlyException("User Not Found!");
            }
            var userRole = await context.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId && x.Role == role);
            if (userRole == null)
            {
                throw new UserFriendlyException("User Doesn't Have Specified Role!");
            }
            context.UserRoles.Remove(userRole);
            await context.SaveChangesAsync();
        }

        public async Task RemoveRoles(long userId)
        {
            var userRoles = await context.UserRoles.Where(x => x.UserId == userId).ToListAsync();
            context.UserRoles.RemoveRange(userRoles);
        }

        public async Task UpdateUserTokenExpired(long userId, string expiredToken)
        {
            var dbUser = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            dbUser.LastExpiredToken = expiredToken;
            await context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user, long id,List<UserRole> userRoles)
        {
            var dbUser = await context.Users
                .Include(m=>m.UserRoles)
                .FirstOrDefaultAsync(x => x.Id == id);

            dbUser.EMail = user.EMail;
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.NationalCode = user.NationalCode;
            dbUser.UserRoles.Clear();
            dbUser.UserRoles = userRoles;
            context.Update(dbUser);
            
            await context.SaveChangesAsync();
        }
    }
}
