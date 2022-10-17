using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.Repository
{
    public class PreRequestRepository: IPreRequestRepository
    {
        private readonly CoreDomainContext _context;

        public PreRequestRepository(CoreDomainContext context)
        {
            _context = context;
        }
        public void Add(PreRequest preRequest)
        {
            _context.PreRequests.AddAsync(preRequest);
        }

        public Task<PreRequest> Find(long id)
        {
            return _context.PreRequests.SingleOrDefaultAsync(m => m.Id==id);
        }

        public void Update(PreRequest preRequest)
        {
            _context.PreRequests.Update(preRequest);
        }

        public void Delete(PreRequest preRequest)
        {
            _context.PreRequests.Remove(preRequest);
        }
    }
}