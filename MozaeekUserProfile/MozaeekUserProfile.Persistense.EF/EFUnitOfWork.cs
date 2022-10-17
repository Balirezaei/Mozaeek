using MozaeekUserProfile.Core.Core;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;

namespace MozaeekUserProfile.Persistense.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly MozaeekUserProfileContext _context;

        public EFUnitOfWork(MozaeekUserProfileContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
        private IDbContextTransaction Transaction { get; set; }
        public Task CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task BeginTransaction()
        {
            Transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            if (Transaction != null)
            {
                await Transaction.CommitAsync();
            }

        }
        public async Task RollbackTransaction()
        {
            if (Transaction != null)
            {
                await Transaction.RollbackAsync();
            }
        }
    }
}