using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.Repository
{
    public class UserWalletRepository : IUserWalletRepository
    {
        private readonly MozaeekUserProfileContext _context;

        public UserWalletRepository(MozaeekUserProfileContext context)
        {
            _context = context;
        }

        public Task<UserWallet> GetWithDebit(long userId, long currencyType)
        {
            return _context.UserWallets.Where(
                    m => m.UserId == userId && m.PriceCurrencyId == currencyType)
                .Include(m => m.UserWalletDebits)
                .FirstOrDefaultAsync();
        }

        public Task<UserWallet> GetWithDebitByQuestion(long questionId)
        {
            return _context.UserWallets.Where(m => m.UserWalletDebits.Any(z => z.UserQuestionId == questionId))
                .Include(m => m.UserWalletDebits)
                .FirstOrDefaultAsync();
        }

        public Task<UserWallet> GetByCurrency(long userId, long currencyType)
        {
            return _context.UserWallets.Where(
                    m => m.UserId == userId && m.PriceCurrencyId == currencyType)
                .FirstOrDefaultAsync();
        }

        public void UpdateUserWallet(UserWallet userWallet)
        {
            _context.UserWallets.Update(userWallet);
        }
    }
}