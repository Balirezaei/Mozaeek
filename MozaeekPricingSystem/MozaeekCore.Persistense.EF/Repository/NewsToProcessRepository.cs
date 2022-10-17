using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.IdGenerators;
using MozaeekCore.QueryModel;
using MozaeekCore.ViewModel;

namespace MozaeekCore.Persistense.EF.Repository
{
    public interface INewsToProcessRepository
    {
        Task<List<RSSNews>> GetAllNewsReadyToProcess(PagingContract pagingContract);
    }

    public class NewsToProcessRepository: INewsToProcessRepository
    {
        private readonly CoreDomainContext _context;

        public NewsToProcessRepository(CoreDomainContext context)
        {
            _context = context;
        }

        public Task<List<RSSNews>> GetAllNewsReadyToProcess(PagingContract pagingContract)
        {
            return _context.RssNewses.Where(m => m.IsProcessed == false)
                .Skip((pagingContract.PageSize * (pagingContract.PageNumber - 1)))
                .Take(pagingContract.PageSize).ToListAsync();
        }
    }
}