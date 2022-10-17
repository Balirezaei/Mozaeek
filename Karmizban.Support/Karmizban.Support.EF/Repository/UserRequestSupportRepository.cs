using System.Threading.Tasks;
using Karmizban.Support.Domain;
using Karmizban.Support.EF.ContextContainer;
using Microsoft.EntityFrameworkCore;

namespace Karmizban.Support.EF.Repository
{
    public class UserRequestSupportRepository : IUserRequestSupportRepository
    {
        private readonly SupportContext _context;

        public UserRequestSupportRepository(SupportContext context)
        {
            _context = context;
        }

        public async Task Create(UserRequestSupport domain)
        {
            await _context.UserRequestSupports.AddAsync(domain);
        }

        public Task<UserRequestSupport> Find(long id)
        {
            return _context.UserRequestSupports
                .Include(m => m.UserSuggestedSupport)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}