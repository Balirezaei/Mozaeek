using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly CoreDomainContext _context;

        public RequestRepository(CoreDomainContext context)
        {
            _context = context;
        }
        public void Add(Request request)
        {
            _context.Requests.Add(request);
        }

        public Task<Request> FindWithAssociation(long id)
        {
            return _context.Requests.Where(m => m.Id == id)
                .Include(m => m.RequestTarget)
                .Include(m => m.RequestPoints)
                .Include(m => m.RequestQualifications)
                .Include(m => m.RequestDefiniteRequestOrgs)
                .Include(m => m.RequestRequestOrgs)
                .Include(m => m.RequestNecessities)
                .Include(m => m.RequestActions)
                .Include(m => m.RequestTargetConnections)
                .FirstOrDefaultAsync();
        }
        public Task<Request> FindWithoutAssociation(long id)
        {
            return _context.Requests.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public void Delete(Request request)
        {
            _context.Requests.Remove(request);
        }

        public void Update(Request request)
        {
            _context.Requests.Update(request);
        }
    }
}