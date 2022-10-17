using System.Threading.Tasks;

namespace MozaeekCore.Domain
{
    public interface IUnProcessedRequestRepository
    {
        void Add(UnProcessedRequest unProcessedRequest);
        Task<UnProcessedRequest> Find(long id);
    }
}