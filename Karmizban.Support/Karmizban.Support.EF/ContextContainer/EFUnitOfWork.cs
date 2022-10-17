using System.Threading.Tasks;

namespace Karmizban.Support.EF.ContextContainer
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly SupportContext _context;

        public EFUnitOfWork(SupportContext context)
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