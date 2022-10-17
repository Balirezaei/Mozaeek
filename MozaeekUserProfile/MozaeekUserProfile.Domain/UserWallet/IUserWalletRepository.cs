using System.Threading.Tasks;

namespace MozaeekUserProfile.Domain
{
    public interface IUserWalletRepository
    {
        Task<UserWallet> GetWithDebit(long userId, long currencyType);
        Task<UserWallet> GetWithDebitByQuestion(long questionId);
        Task<UserWallet> GetByCurrency(long userId, long currencyType);
        void UpdateUserWallet(UserWallet userWallet);
    }
}

