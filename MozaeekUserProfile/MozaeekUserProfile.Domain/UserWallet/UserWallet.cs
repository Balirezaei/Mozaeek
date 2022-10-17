using System.Collections.Generic;

namespace MozaeekUserProfile.Domain
{
    public class UserWallet
    {
        protected UserWallet() { }

        public int Id { get; private set; }
        public long UserId { get; private set; }
        public virtual User User { get; private set; }
        public ICollection<UserWalletCredit> UserWalletCredits { get; private set; }
        public ICollection<UserWalletDebit> UserWalletDebits { get; private set; }
        public string PriceCurrencyTitle { get; private set; }
        public int PriceCurrencyId { get; private set; }

        /// <summary>
        /// موجودی
        /// </summary>
        public int AvailableAmount { get; private set; }

        public void IncreaseUserCredit(UserWalletCreditType creditType, string description, int amount)
        {
            UserWalletCredits ??= new List<UserWalletCredit>();
            UserWalletCredits.Add(new UserWalletCredit(description, creditType, amount));
            AvailableAmount += amount;
        }

        public void DecreaseUserCredit(long questionId, int amount, string description)
        {
            UserWalletDebits ??= new List<UserWalletDebit>();
            UserWalletDebits.Add(new UserWalletDebit(description, questionId, amount));
            AvailableAmount -= amount;
        }
    }
}