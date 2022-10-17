using System;

namespace MozaeekUserProfile.Domain
{
    public class UserWalletCredit
    {
        protected UserWalletCredit()
        {
        }

        public UserWalletCredit(string description, UserWalletCreditType userWalletCreditType, int amount)
        {
            Description = description;
            UserWalletCreditType = userWalletCreditType;
            Amount = amount;
            CreateDate = DateTime.Now;
        }

        public long Id { get; private set; }
        public virtual UserWallet UserWallet { get; private set; }
        public int UserWalletId { get; private set; }
        public DateTime CreateDate { get; private set; }
        public string Description { get; private set; }
        public UserWalletCreditType UserWalletCreditType { get; private set; }
        public int Amount { get; private set; }
    }
}