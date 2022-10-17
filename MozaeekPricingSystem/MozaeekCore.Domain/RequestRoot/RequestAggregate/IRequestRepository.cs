using System.Threading.Tasks;

namespace MozaeekCore.Domain
{
    public interface IRequestRepository
    {
        void Add(Request request);
        Task<Request> Find(long id);
        void Delete(Request request);
    }
}