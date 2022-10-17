using System.Threading.Tasks;

namespace MozaeekUserProfile.Core
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}