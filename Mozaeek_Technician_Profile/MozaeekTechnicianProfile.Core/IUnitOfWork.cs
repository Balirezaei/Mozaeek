using System.Threading.Tasks;

namespace MozaeekTechnicianProfile.Core
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}