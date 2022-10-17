using System;
using System.Collections.Generic;
using Karmizban.Support.Common;

namespace Karmizban.Support.Domain
{
    public class UserRequestSupport
    {
        public long Id { get; private set; }
        public User User { get; private set; }
        public UserSuggestedSupport UserSuggestedSupport { get; private set; }
        public long? UserSuggestedSupportId { get; private set; }
        public long QuestionId { get; private set; }
        public string QuestionCode { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreateDate { get; private set; }
        public bool IsAnswered { get; private set; }
        public UserRequestSupportAnswer UserRequestSupportAnswer { get; private set; }

        public UserRequestSupport(User user, long? userSuggestedSupportId, long questionId, string questionCode, string title, string description)
        {
            User = user;
            UserSuggestedSupportId = userSuggestedSupportId;
            QuestionId = questionId;
            QuestionCode = questionCode;
            Title = title;
            Description = description.Recheck();
            CreateDate = DateTime.Now;
            IsAnswered = false;
        }
        protected UserRequestSupport() { }

        public void Answer(UserRequestSupportAnswer answer)
        {
            if (IsAnswered)
            {
                throw new Exception("پرسش قبلاً پاسخ داده شده است.");
            }
            this.UserRequestSupportAnswer = answer;
            this.IsAnswered = true;
        }
    }
}