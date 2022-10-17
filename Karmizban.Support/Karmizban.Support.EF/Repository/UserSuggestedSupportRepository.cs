using System.Collections.Generic;
using System.Threading.Tasks;
using Karmizban.Support.Domain;
using Karmizban.Support.EF.ContextContainer;
using Microsoft.EntityFrameworkCore;

namespace Karmizban.Support.EF.Repository
{
    public class UserSuggestedSupportRepository : IUserSuggestedSupportRepository
    {
        private readonly SupportContext _context;

        public UserSuggestedSupportRepository(SupportContext context)
        {
            _context = context;
        }

        public async Task CreateUserSuggest(UserSuggestedSupport domain)
        {
            await _context.UserSuggestedSupports.AddAsync(domain);
        }

        public async Task<List<UserSuggestedSupport>> GetAll()
        {
            return await _context.UserSuggestedSupports.ToListAsync();
        }

        public Task<UserSuggestedSupport> Find(long id)
        {
            return _context.UserSuggestedSupports.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}