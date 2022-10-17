using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.Repository
{
    public class UnProcessedRequestRepository: IUnProcessedRequestRepository
    {
        private readonly CoreDomainContext _context;

        public UnProcessedRequestRepository(CoreDomainContext context)
        {
            _context = context;
        }
        public void Add(UnProcessedRequest unProcessedRequest)
        {
            _context.UnProcessedRequests.AddAsync(unProcessedRequest);
        }

        public Task<UnProcessedRequest> Find(long id)
        {
            return _context.UnProcessedRequests.SingleOrDefaultAsync(m => m.Id==id);
        }
    }
}