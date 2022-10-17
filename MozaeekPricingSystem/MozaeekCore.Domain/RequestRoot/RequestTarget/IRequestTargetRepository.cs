using System.Threading.Tasks;

namespace MozaeekCore.Domain
{
    public interface IRequestTargetRepository
    {
        Task CreatRequestTarget(RequestTarget requestTarget);
        void UpdateRequestTarget(RequestTarget requestTarget);
        Task<RequestTarget> Find(long id);
        void ResetAssociations(RequestTarget requestTarget);
        void Delete(RequestTarget requestTarget);
        Task<bool> CanBeDeletedByAnouncement(long id);
        Task<bool> CanBeDeletedByRequest(long id);
    }
}