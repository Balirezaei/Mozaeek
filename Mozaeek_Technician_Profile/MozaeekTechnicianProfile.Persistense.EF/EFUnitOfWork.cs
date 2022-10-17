using MozaeekTechnicianProfile.Core.Core;
using System.Threading.Tasks;

namespace MozaeekTechnicianProfile.Persistense.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly MozaeekTechnicianProfileContext _context;

        public EFUnitOfWork(MozaeekTechnicianProfileContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public Task CommitAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}