using System.Threading.Tasks;

namespace MozaeekTechnicianProfile.Core.Core
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}