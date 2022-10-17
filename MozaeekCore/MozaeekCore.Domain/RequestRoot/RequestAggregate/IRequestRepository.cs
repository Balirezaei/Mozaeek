using System.Threading.Tasks;

namespace MozaeekCore.Domain
{
    public interface IRequestRepository
    {
        void Add(Request request);
        Task<Request> FindWithAssociation(long id);
        Task<Request> FindWithoutAssociation(long id);
        void Delete(Request request);
        void Update(Request request);
    }
}