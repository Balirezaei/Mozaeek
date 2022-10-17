using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.QueryModel;
using MozaeekCore.ViewModel;

namespace MozaeekCore.Persistense.EF.Repository
{
    public interface INewsToProcessRepository
    {
        Task<List<RSSNews>> GetAllNewsReadyToProcess(PagingContract pagingContract);
        Task<RSSNews> GetRssNewsById(long id);
        Task<long> GetTotalCountForProcess();
        // Task<long> GetTotalCountNewsRequest();
    }

    public class NewsToProcessRepository : INewsToProcessRepository
    {
        private readonly CoreDomainContext _context;

        public NewsToProcessRepository(CoreDomainContext context)
        {
            _context = context;
        }

        public Task<List<RSSNews>> GetAllNewsReadyToProcess(PagingContract pagingContract)
        {
            return _context.RssNewses.Where(m => m.IsProcessed == false)
                .OrderByDescending(m=>m.Id)
                .Skip((pagingContract.PageSize * (pagingContract.PageNumber - 1)))
                .Take(pagingContract.PageSize).ToListAsync();
        }


        public Task<RSSNews> GetRssNewsById(long id)
        {
            return _context.RssNewses.FirstOrDefaultAsync(m => m.IsProcessed == false && m.Id == id);
        }

        public async Task<long> GetTotalCountForProcess()
        {
            return await _context.RssNewses.Where(m => m.IsProcessed == false).CountAsync();
        }

        // public async Task<long> GetTotalCountNewsRequest()
        // {
        //     return await _context.RssNewses.Where(m => m.IsRequest == true).CountAsync();
        // }
    }
}