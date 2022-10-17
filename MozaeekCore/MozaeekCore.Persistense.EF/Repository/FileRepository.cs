using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly CoreDomainContext _context;

        public FileRepository(CoreDomainContext context)
        {
            _context = context;
        }

        public async Task Create(MosaikFile file)
        {
            await _context.Files.AddAsync(file);
        }

        public void Delete(MosaikFile file)
        {
            _context.Files.Remove(file);
        }

        public Task<MosaikFile> Find(long id)
        {
            return _context.Files.SingleOrDefaultAsync(m => m.Id == id);
        }
    }
}

