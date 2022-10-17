using System.Threading.Tasks;

namespace MozaeekCore.Domain
{
    public interface IPreRequestRepository
    {
        void Add(PreRequest preRequest);
        Task<PreRequest> Find(long id);
        void Update(PreRequest preRequest);
        void Delete(PreRequest preRequest);
    }
}