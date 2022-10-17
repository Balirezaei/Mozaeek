using System;
using System.Collections.Generic;

namespace MozaeekUserProfile.Domain
{
    public class UserWalletDebit
    {
        protected UserWalletDebit()
        {
        }

        public UserWalletDebit(string description, long questionId, int amount)
        {
            Description = description;
            UserQuestionId = questionId;
            Amount = amount;
            CreateDate = DateTime.Now;
        }

        public long Id { get; private set; }
        public string Description { get; private set; }
        /// <summary>
        /// درحال حاضر فقط با پرسیدن سوال از اعتبار کاربر کسر می شود.
        /// </summary>
        public long UserQuestionId { get; private set; }
        public virtual UserQuestion UserQuestions { get; private set; }
        public virtual UserWallet UserWallet { get; private set; }
        public int UserWalletId { get; private set; }
        public DateTime CreateDate { get; private set; }
        public int Amount { get; private set; }


    }
}