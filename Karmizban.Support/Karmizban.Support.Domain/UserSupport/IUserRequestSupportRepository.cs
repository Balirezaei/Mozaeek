using System.Threading.Tasks;

namespace Karmizban.Support.Domain
{
    public interface IUserRequestSupportRepository
    {
        Task Create(UserRequestSupport domain);
        Task<UserRequestSupport> Find(long id);
    }
}