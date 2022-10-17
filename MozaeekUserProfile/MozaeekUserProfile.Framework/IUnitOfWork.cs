using System.Threading.Tasks;

namespace MozaeekUserProfile.Core.Core
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
    }
}