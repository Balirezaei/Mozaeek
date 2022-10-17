using System.Collections.Generic;
using System.Threading.Tasks;

namespace Karmizban.Support.Domain
{
    public interface IUserSuggestedSupportRepository
    {
        Task CreateUserSuggest(UserSuggestedSupport domain);

        Task<List<UserSuggestedSupport>> GetAll();
        Task<UserSuggestedSupport> Find(long id);
    }
}