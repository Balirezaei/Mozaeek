using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.Repository
{
    public class RequestTargetRepository : IRequestTargetRepository
    {
        private readonly CoreDomainContext _context;

        public RequestTargetRepository(CoreDomainContext context)
        {
            _context = context;
        }
        public async Task CreatRequestTarget(RequestTarget requestTarget)
        {
            await _context.RequestTargets.AddAsync(requestTarget);
        }
        public void UpdateRequestTarget(RequestTarget requestTarget)
        {
            _context.RequestTargets.Update(requestTarget);
        }
        public Task<RequestTarget> Find(long id)
        {
            return _context.RequestTargets.Where(m => m.Id == id)
                .Include(m => m.RequestTargetSubjects)
                .Include(m => m.RequestTargetLabels)
                //.Include(m => m.RequestTargetRequestOrgs)
                .FirstOrDefaultAsync();
        }

        public void ResetAssociations(RequestTarget requestTarget)
        {
            foreach (var label in requestTarget.RequestTargetLabels)
            {
                _context.RequestTargetLabels.Remove(label);
            }

            foreach (var subject in requestTarget.RequestTargetSubjects)
            {
                _context.RequestTargetSubjects.Remove(subject);
            }

            //foreach (var requestOrg in requestTarget.RequestTargetRequestOrgs)
            //{
            //    _context.RequestTargetRequestOrgs.Remove(requestOrg);
            //}

        }

        public void Delete(RequestTarget requestTarget)
        {
            _context.RequestTargets.Remove(requestTarget);
        }

        public Task<bool> CanBeDeletedByRequest(long id)
        {
            return (_context.Requests.AnyAsync(m => m.RequestTargetId == id));
        }

    }
}