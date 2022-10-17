using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.Domain.Pricing;

namespace MozaeekCore.Persistense.EF.Repository
{
    public class RequestPriceRepository : IRequestPriceRepository
    {
        private readonly CoreDomainContext _context;

        public RequestPriceRepository(CoreDomainContext context)
        {
            _context = context;
        }

        public async Task CreateRequestPrice(RequestPriceHeader requestPrice)
        {
            await _context.RequestPriceHeaders.AddAsync(requestPrice);
        }

        public Task<RequestPriceHeader> Find(long id)
        {
            return _context.RequestPriceHeaders
                .Where(m => m.Id == id)
                .Include(m => m.PriceDetails)
                .FirstOrDefaultAsync();
        }

        public void Update(RequestPriceHeader requestPrice)
        {
            _context.RequestPriceHeaders.Update(requestPrice);
        }

        public void Delete(RequestPriceHeader requestPrice)
        {
            _context.RequestPriceHeaders.Remove(requestPrice);
        }
    }
}