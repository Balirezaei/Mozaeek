using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.Repository
{
    public class UserProfileCharacteristicRepository : IUserProfileCharacteristicRepository
    {
        private readonly MozaeekUserProfileContext _dbContext;

        public UserProfileCharacteristicRepository(MozaeekUserProfileContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserProfileCharacteristicOwner> CreateUserProfileCharacteristicOwner(UserProfileCharacteristicOwner domain)
        {
            var preSave =
                await _dbContext.UserProfileCharacteristicOwners.FirstOrDefaultAsync(m =>
                    m.Name == domain.Name && m.UserId == domain.UserId);
            if (preSave != null)
            {
                return preSave;
            }
            await _dbContext.UserProfileCharacteristicOwners.AddAsync(domain);
            return domain;
        }

        public async Task DeleteUserProfileCharacteristicOwner(int id)
        {
            var domain = await _dbContext.UserProfileCharacteristicOwners
                .Include(m => m.UserProfileCharacteristics)
                .ThenInclude(m => m.UserDashboardCharacteristics)
                .FirstOrDefaultAsync(m => m.Id == id);

            _dbContext.UserDashboardCharacteristics.RemoveRange(domain.UserProfileCharacteristics.SelectMany(m => m.UserDashboardCharacteristics));
            _dbContext.UserProfileCharacteristicOwners.Remove(domain);
        }

        public void DeleteUserProfileCharacteristic(UserProfileCharacteristic domain)
        {
            _dbContext.UserProfileCharacteristics.Remove(domain);
        }

        public async Task CreateUserProfileCharacteristic(UserProfileCharacteristic domain)
        {
            await _dbContext.UserProfileCharacteristics.AddAsync(domain);
        }

        public async Task<UserDashboardCharacteristic> CreateUserDashboardCharacteristic(UserDashboardCharacteristic domain)
        {
            await _dbContext.UserDashboardCharacteristics.AddAsync(domain);
            return domain;
        }

        public Task<UserProfileCharacteristic> FindCharacteristic(int inputCharacteristicId)
        {
            return _dbContext.UserProfileCharacteristics.Where(m => m.Id == inputCharacteristicId)
                .Include(m => m.UserProfileCharacteristicOwner)
                .FirstOrDefaultAsync();
        }

        public Task<List<UserProfileCharacteristicOwner>> GetAllUserProfileCharacteristicOwner(long userId)
        {
            return _dbContext.UserProfileCharacteristicOwners.Where(m => m.UserId == userId).ToListAsync();
        }

        public Task<List<UserProfileCharacteristic>> GetAllUserProfileCharacteristic(long userId)
        {
            return _dbContext.UserProfileCharacteristics
                .Where(m => m.UserProfileCharacteristicOwner.UserId == userId)
                .Include(m => m.UserProfileCharacteristicOwner)
                .ToListAsync();
        }

        public IQueryable<UserDashboardCharacteristic> GetAllUserDashboardCharacteristic(long userId)
        {
            return _dbContext.UserDashboardCharacteristics
                .Include(m=>m.UserProfileCharacteristic)
                .Where(m => m.UserId == userId);
        }

        public async Task RemoveUserDashboardCharacteristic(int ownerId)
        {
            var list = await _dbContext.UserDashboardCharacteristics
                .Where(m => m.UserProfileCharacteristic.UserProfileCharacteristicOwnerId == ownerId).ToListAsync();
            foreach (var item in list)
            {
                _dbContext.UserDashboardCharacteristics.Remove(item);
            }
        }

        public Task<List<UserDashboardCharacteristic>> GetAllUserDashboardCharacteristicByOwner(int ownerId)
        {
            return _dbContext.UserDashboardCharacteristics
                .Include(m=>m.UserProfileCharacteristic)
                .Where(m => m.UserProfileCharacteristic.UserProfileCharacteristicOwnerId == ownerId)
                .ToListAsync();
        }

        public Task<List<UserProfileCharacteristic>> GetAllUserProfileCharacteristicByOwner(int ownerId)
        {
            return _dbContext.UserProfileCharacteristics
                .Where(m => m.UserProfileCharacteristicOwnerId == ownerId)
                .Include(m => m.UserProfileCharacteristicOwner)
                .ToListAsync();
        }

        public Task<List<UserProfileCharacteristic>> GetAllUserProfileCharacteristicByPredicate(Expression<Func<UserProfileCharacteristic, bool>> predicate)
        {
            return _dbContext.UserProfileCharacteristics.Where(predicate).ToListAsync();
        }

        public Task<UserProfileCharacteristicOwner> FindCharacteristicOwner(int ownerId)
        {
            return _dbContext.UserProfileCharacteristicOwners.FirstOrDefaultAsync(m => m.Id == ownerId);
        }
    }
}