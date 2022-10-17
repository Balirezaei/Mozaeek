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

        public Task<Request> Find(long id)
        {
            return _context.Requests.Where(m => m.Id == id)
                .Include(m => m.RequestTarget)
                .Include(m => m.RequestPoints).FirstOrDefaultAsync();
        }

        public void Delete(Request request)
        {
            _context.Requests.Remove(request);
        }
    }
}